using FitTrackerApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Interface
{
    public interface IMealItemService
    {
        Task<List<MealDetails>> GetAll();
        Task<MealDetails?> GetById(Guid id);
        Task<MealDetails> Insert(MealDetails mealItem);
        Task<ICollection<MealDetails>> InsertMany(ICollection<MealDetails> mealItems);
        Task<MealDetails> Update(MealDetails mealItem);
        Task<MealDetails> DeleteById(Guid id);
        Task<List<MealDetails>> GetAllByUserId(string userId);
    }
}
