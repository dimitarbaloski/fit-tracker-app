using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackerApp.Domain.DomainModels;



namespace FitTrackerApp.Domain.Identity_Models
{
    public class FitTrackerAppUser : IdentityUser
    {
        public string FullName { get; set; }
        public int? Age { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }

        public string? Gender { get; set; }

        public ICollection<Meal> Meals { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public ICollection<ProgressEntry> ProgressEntries { get; set; }
    }
}
