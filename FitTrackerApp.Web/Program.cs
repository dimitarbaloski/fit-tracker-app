using FitTrackerApp.Repository.Implementation;
using FitTrackerApp.Data;
using FitTrackerApp.Domain.Identity_Models;
using FitTrackerApp.Repository.Interface;
using FitTrackerApp.Service.Implementation;
using FitTrackerApp.Service.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using CoursesApplication.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<FitTrackerAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PasswordHasher<FitTrackerAppUser>>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMealRepository, MealRepository>();

builder.Services.AddTransient<IMealItemService, MealItemService>();
builder.Services.AddTransient<IMealService, MealService>();
builder.Services.AddTransient<IWorkoutService, WorkoutService>();
builder.Services.AddTransient<IProgressEntryService, ProgressEntryService>();
builder.Services.AddTransient<IDataFetchService, DataFetchService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseMigrationsEndPoint();
}
else
{
app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();
}

if (app.Environment.IsEnvironment("Test"))
{
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ApplicationDbContext>();
var manager = scope.ServiceProvider.GetRequiredService<UserManager<FitTrackerAppUser>>();
var passwordHasher = services.GetRequiredService<PasswordHasher<FitTrackerAppUser>>();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();
await ApplicationDbContextSeeder.SeedDatabase(context, manager, passwordHasher);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapRazorPages();

app.Run();

public partial class Program { }


