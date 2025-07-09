using FitTrackerApp.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Domain.DomainModels
{
    public class MealDetails : BaseEntity
    {
        public string FoodName { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbs { get; set; }

        public string Query { get; set; }

        public string UserId { get; set; }             
        public FitTrackerAppUser? User { get; set; }  



    }
}
