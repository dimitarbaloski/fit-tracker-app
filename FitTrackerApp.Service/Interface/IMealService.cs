using FitTrackerApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Interface
{
    public interface IMealService
    {
        Task<List<Meal>> GetAll();
        Task<Meal?> GetById(Guid id);
        Task<Meal> Insert(Meal meal);
        Task<ICollection<Meal>> InsertMany(ICollection<Meal> meals);
        Task<Meal> Update(Meal meal);
        Task<Meal> DeleteById(Guid id);
        Task<List<Meal>> GetAllByUserId(string userId);
        Task<IEnumerable<Meal>> GetAllByUserIdWithItemsAsync(string userId);

        Task<Meal> AddMealWithItemsAsync(Meal meal, List<String> items);
    }
}
