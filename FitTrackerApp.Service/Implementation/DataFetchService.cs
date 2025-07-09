using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Domain.DTO;
using FitTrackerApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FitTrackerApp.Service.Implementation
{
    public class DataFetchService : IDataFetchService
    {
        private readonly IMealService _mealService;
        private readonly HttpClient _httpClient;

        public DataFetchService(IMealService mealService, IHttpClientFactory httpClientFactory)
        {
            _mealService = mealService;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<MealDetails>> FetchMultipleNutritionDataAsync(IEnumerable<string> queries)
        {
            var results = new List<MealDetails>();

            foreach (var query in queries)
            {
                var trimmedQuery = query.Trim();
                if (string.IsNullOrWhiteSpace(trimmedQuery))
                    continue;

                try
                {
                    var meal = await FetchNutritionDataAsync(trimmedQuery);
                    if (meal != null)
                        results.Add(meal);
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"Failed to fetch data for '{trimmedQuery}': {ex.Message}");
                }
            }

            return results;
        }

        public async Task<MealDetails> FetchNutritionDataAsync(string query)
        {
            var request = new
            {
                query = query
            };

            var requestContent = JsonContent.Create(request);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://trackapi.nutritionix.com/v2/natural/nutrients")
            {
                Content = requestContent
            };

            requestMessage.Headers.Add("x-app-id", "92d96f22");
            requestMessage.Headers.Add("x-app-key", "aa2e33d26a2e7de0b8b25a7c66904071");

            var response = await _httpClient.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<NutritionixResponseDto>();


            var combinedMeal = new MealDetails
            {
                Id = Guid.NewGuid(),
                FoodName = result.foods.First().food_name, 
                Query = query,                            
                Calories = result.foods.Sum(f => f.nf_calories),
                Protein = result.foods.Sum(f => f.nf_protein),
                Carbs = result.foods.Sum(f => f.nf_total_carbohydrate),
                Fat = result.foods.Sum(f => f.nf_total_fat)
            };

            return combinedMeal;

        }


    }
}
