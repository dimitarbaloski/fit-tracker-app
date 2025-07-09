using FitTrackerApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Service.Interface
{
    public interface IDataFetchService
    {
        Task<MealDetails> FetchNutritionDataAsync(string query);

        Task<List<MealDetails>> FetchMultipleNutritionDataAsync(IEnumerable<string> queries);
    }
}
