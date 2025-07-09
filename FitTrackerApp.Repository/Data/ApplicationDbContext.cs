using FitTrackerApp.Domain.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitTrackerApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
