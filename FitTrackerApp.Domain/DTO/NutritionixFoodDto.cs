using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Domain.DTO
{
    public class NutritionixFoodDto
{
    public string food_name { get; set; }
    public float nf_calories { get; set; }
    public float nf_protein { get; set; }
    public float nf_total_carbohydrate { get; set; }
    public float nf_total_fat { get; set; }
}

}
