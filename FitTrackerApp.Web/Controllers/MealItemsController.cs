using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
public class MealItemsController : Controller
{
    private readonly IMealItemService _mealItemService;
    private readonly IMealService _mealService;
    private readonly IDataFetchService _fetchService;

    public MealItemsController(IMealItemService mealItemService, IMealService mealService, IDataFetchService fetchService)
    {
        _mealItemService = mealItemService;
        _mealService = mealService;
        _fetchService = fetchService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var mealItems = await _mealItemService.GetAllByUserId(userId);
        return View(mealItems);
    }

    public async Task<IActionResult> Details()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      
        var meals = await _mealService.GetAllByUserIdWithItemsAsync(userId);

        ViewData["Meals"] = meals;

        var mealItems = await _mealItemService.GetAllByUserId(userId);

        return View(mealItems);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FoodName,Calories,Protein,Fat,Carbs")] MealDetails mealItem)
    {
        if (ModelState.IsValid)
        {
            mealItem.Id = Guid.NewGuid();
            mealItem.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mealItemService.Insert(mealItem);
            return RedirectToAction(nameof(Index));
        }

        return View(mealItem);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var mealItem = await _mealItemService.GetById(id.Value);
        if (mealItem == null) return NotFound();

        return View(mealItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,FoodName,Calories,Protein,Fat,Carbs")] MealDetails mealItem)
    {
        if (id != mealItem.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
               
                if (string.IsNullOrEmpty(mealItem.UserId))
                    mealItem.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _mealItemService.Update(mealItem);
            }
            catch (Exception)
            {
                if (!await MealItemExists(mealItem.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(mealItem);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var mealItem = await _mealItemService.GetById(id.Value);
        if (mealItem == null) return NotFound();

        return View(mealItem);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _mealItemService.DeleteById(id);
        return RedirectToAction(nameof(Details));
    }

    private async Task<bool> MealItemExists(Guid id)
    {
        var mealItem = await _mealItemService.GetById(id);
        return mealItem != null;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FetchMealDetails(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            ModelState.AddModelError("", "Query cannot be empty.");
            return RedirectToAction(nameof(Index));
        }

        var fetchedMeal = await _fetchService.FetchNutritionDataAsync(query);
        fetchedMeal.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

        await _mealItemService.Insert(fetchedMeal);

        return RedirectToAction("Details", new { id = fetchedMeal.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FetchMultipleMeals(string queries)
    {
        if (string.IsNullOrWhiteSpace(queries))
        {
            ModelState.AddModelError("", "Please enter at least one food query.");
            return RedirectToAction(nameof(Details));
        }

        var queryList = queries
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(q => q.Trim())
            .Where(q => !string.IsNullOrWhiteSpace(q))
            .ToList();

        var failedQueries = new System.Collections.Generic.List<string>();

        foreach (var query in queryList)
        {
            try
            {
                var meal = await _fetchService.FetchNutritionDataAsync(query);
                meal.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
                await _mealItemService.Insert(meal);
            }
            catch
            {
                failedQueries.Add(query);
            }
        }

        if (failedQueries.Any())
        {
            TempData["Error"] = "Could not fetch data for: " + string.Join(", ", failedQueries);
        }

        return RedirectToAction(nameof(Details));
    }
}
