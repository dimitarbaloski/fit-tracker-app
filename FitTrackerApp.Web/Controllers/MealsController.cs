using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Domain.Identity_Models;
using FitTrackerApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
public class MealsController : Controller
{
    private readonly IMealService _mealService;
    private readonly UserManager<FitTrackerAppUser> _userManager;

    public MealsController(IMealService mealService, UserManager<FitTrackerAppUser> userManager)
    {
        _mealService = mealService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var meals = await _mealService.GetAllByUserId(userId);
        return View(meals);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();


        var meal = await _mealService.GetById(id.Value);
        if (meal == null) return NotFound();

        return View(meal);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Date")] Meal meal)
    {
        if (ModelState.IsValid)
        {
            meal.Id = Guid.NewGuid();
            meal.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            await _mealService.Insert(meal);
            return RedirectToAction(nameof(Index));
        }
        return View(meal);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateWithItems(Meal meal, List<string> items)
    {
        if (ModelState.IsValid)
        {
            meal.Id = Guid.NewGuid();
            meal.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            await _mealService.AddMealWithItemsAsync(meal, items);
            return RedirectToAction(nameof(Index));
        }
        return View(meal);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var meal = await _mealService.GetById(id.Value);
        if (meal == null) return NotFound();

        return View(meal);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Date,UserId")] Meal meal)
    {
        if (id != meal.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                
                if (string.IsNullOrEmpty(meal.UserId))
                    meal.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _mealService.Update(meal);
            }
            catch (Exception)
            {
                if (!await MealExists(meal.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(meal);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var meal = await _mealService.GetById(id.Value);
        if (meal == null) return NotFound();

        return View(meal);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var meal = await _mealService.GetById(id);
        if (meal != null)
        {
            await _mealService.DeleteById(id);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MealExists(Guid id)
    {
        var meal = await _mealService.GetById(id);
        return meal != null;
    }

    public IActionResult CreateWithItems()
    {
        return View();
    }
}
