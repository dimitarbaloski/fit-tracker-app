using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Repository.Interface;
using FitTrackerApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Implementation
{
    public class MealService : IMealService
    {
        private readonly IRepository<Meal> _mealRepository;
        private readonly IMealItemService _mealItemService;

        public MealService(IRepository<Meal> mealRepository, IMealItemService mealItemService) { 
            
            _mealRepository = mealRepository; 
            _mealItemService = mealItemService;
        
        }
        public async Task<Meal> AddMealWithItemsAsync(Meal meal, List<String> items)
        {
            meal.Items = items;
            return await _mealRepository.InsertAsync(meal);
        }

        public async Task<Meal> DeleteById(Guid id)
        {
            var meal = await _mealRepository.GetAsync(selector: x => x,
                predicate: x => x.Id == id);

            if (meal == null) {

                throw new Exception($"Meal with id {id} not found!");
            }

            return await _mealRepository.DeleteAsync(meal);
        }

        public async Task<List<Meal>> GetAll()
        {
            var meals = await _mealRepository.GetAllAsync(selector: x => x);               

            return meals.ToList();
        }

        public async Task<Meal?> GetById(Guid id)
        {
            var meal = await _mealRepository.GetAsync(selector: x => x,
                predicate: x => x.Id == id);

            if (meal == null)
            {
                throw new Exception($"Meal with id {id} not found!");
            }

            return meal;
        }

        public async Task<Meal> Insert(Meal meal)
        {
            return await _mealRepository.InsertAsync(meal); 
        }

        public async Task<ICollection<Meal>> InsertMany(ICollection<Meal> meals)
        {
            return await _mealRepository.InsertManyAsync(meals);
        }

        public async Task<Meal> Update(Meal meal)
        {
            return await _mealRepository.UpdateAsync(meal);
        }
        public async Task<List<Meal>> GetAllByUserId(string userId)
        {
            var meals = await _mealRepository.GetAllAsync(
                selector: x => x,
                predicate: m => m.UserId == userId
            );

            return meals.ToList();
        }

        public async Task<IEnumerable<Meal>> GetAllByUserIdWithItemsAsync(string userId)
        {
            var meals = await _mealRepository.GetAllAsync(
                selector: m => m,
                predicate: m => m.UserId == userId
            );

            return meals;
        }

    }
}
