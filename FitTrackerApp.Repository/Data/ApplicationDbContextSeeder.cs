using FitTrackerApp.Data;
using FitTrackerApp.Domain.Identity_Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

public static class ApplicationDbContextSeeder
{
    public static async Task SeedDatabase(ApplicationDbContext context, UserManager<FitTrackerAppUser> userManager, PasswordHasher<FitTrackerAppUser> passwordHasher)
    {
        context.Database.EnsureCreated();

        if (await userManager.FindByEmailAsync("admin@example.com") != null)
            return;

        var adminUser = new FitTrackerAppUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(adminUser, "Admin123!"); 

        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors)}");
        }

        
    }
}
