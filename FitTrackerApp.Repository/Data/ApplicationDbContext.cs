using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Domain.Identity_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitTrackerApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<FitTrackerAppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Meal> Meals {  get; set; }
        public DbSet<MealDetails> MealItems { get; set; }
        public DbSet<ProgressEntry> ProgressEntries { get; set; }
        public DbSet<Workout> Workouts { get; set; }

    }
}
