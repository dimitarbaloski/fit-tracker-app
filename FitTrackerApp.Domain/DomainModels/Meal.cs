using FitTrackerApp.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrackerApp.Domain.DomainModels
{
    public class Meal : BaseEntity
    {
        public string Name { get; set; }  
        public DateTime Date { get; set; }
        public string? UserId { get; set; }
        public FitTrackerAppUser? User { get; set; }

        public List<String> Items { get; set; } = new List<String>();

    }
}
