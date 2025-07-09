using FitTrackerApp.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Domain.DomainModels
{
    public class Workout : BaseEntity
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
        public float EstimatedCaloriesBurned { get; set; }

        public string? UserId { get; set; }
        public FitTrackerAppUser? User { get; set; }

    }
}
