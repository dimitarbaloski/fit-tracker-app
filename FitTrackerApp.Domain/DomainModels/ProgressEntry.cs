using FitTrackerApp.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Domain.DomainModels
{
    public class ProgressEntry : BaseEntity
    {
        public DateTime Date { get; set; }
        public float WeightKg { get; set; }
        public float BodyFatPercentage { get; set; }

        public string? UserId { get; set; }
        public FitTrackerAppUser? User { get; set; }

    }
}
