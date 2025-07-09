using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Repository.Implementation;
using FitTrackerApp.Repository.Interface;
using FitTrackerApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Implementation
{
    public class MealItemService : IMealItemService
    {
        private readonly IRepository<MealDetails> _mealItemRepository;

        public MealItemService(IRepository<MealDetails> mealItemRepository)
        {
            _mealItemRepository = mealItemRepository;
        }

        public async Task<MealDetails> DeleteById(Guid id)
        {
            var mealItem = await _mealItemRepository.GetAsync(selector: x => x,
        predicate: x => x.Id == id);

            if (mealItem == null)
            {
                throw new Exception($"MealItem with id {id} not found.");
            }

            return await _mealItemRepository.DeleteAsync(mealItem);
        }

        public async Task<List<MealDetails>> GetAll()
        {
            var mealItems = await _mealItemRepository.GetAllAsync(selector: x => x);
            return mealItems.ToList();
        }

        public async Task<MealDetails?> GetById(Guid id)
        {
            var mealItem = await _mealItemRepository.GetAsync(selector: x => x,
        predicate: x => x.Id == id);

            if (mealItem == null)
            {
                throw new Exception($"MealItem with id {id} not found.");
            }

            return mealItem;
        }

      
        public async Task<MealDetails> Insert(MealDetails mealItem)
        {
            return await _mealItemRepository.InsertAsync(mealItem);
        }

        public async Task<ICollection<MealDetails>> InsertMany(ICollection<MealDetails> mealItems)
        {
            return await _mealItemRepository.InsertManyAsync(mealItems);
        }

        public async Task<MealDetails> Update(MealDetails mealItem)
        {
            return await _mealItemRepository.UpdateAsync(mealItem);
        }

        public async Task<List<MealDetails>> GetAllByUserId(string userId)
        {
            var meals = await _mealItemRepository.GetAllAsync(
                selector: x => x,
                predicate: m => m.UserId == userId
            );

            return meals.ToList();
        }
    }
}
