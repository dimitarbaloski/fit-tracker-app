using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class WorkoutsController : Controller
{
    private readonly IWorkoutService _workoutService;

    public WorkoutsController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var workouts = await _workoutService.GetByUserIdAsync(userId);
        return View(workouts);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();

        var workout = await _workoutService.GetById(id.Value);
        if (workout == null) return NotFound();

        return View(workout);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Type,Date,DurationMinutes,EstimatedCaloriesBurned,UserId,Id")] Workout workout)
    {
        if (ModelState.IsValid)
        {
            workout.Id = Guid.NewGuid();
            workout.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            await _workoutService.Insert(workout);
            return RedirectToAction(nameof(Index));
        }
        return View(workout);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var workout = await _workoutService.GetById(id.Value);
        if (workout == null) return NotFound();

        return View(workout);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Type,Date,DurationMinutes,EstimatedCaloriesBurned,UserId,Id")] Workout workout)
    {
        if (id != workout.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                if (string.IsNullOrEmpty(workout.UserId))
                    workout.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _workoutService.Update(workout);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(workout.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(workout);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var workout = await _workoutService.GetById(id.Value);
        if (workout == null) return NotFound();

        return View(workout);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var workout = await _workoutService.GetById(id);
        if (workout != null)
        {
            await _workoutService.DeleteById(workout.Id);
        }
        return RedirectToAction(nameof(Index));
    }

    private bool WorkoutExists(Guid id)
    {
        return _workoutService.GetById(id) != null;
    }

    public IActionResult FilterByDate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterByDate(DateTime from, DateTime to)
    {
        if (from > to)
        {
            ModelState.AddModelError("", "'From' date must be earlier than or equal to 'To' date.");
            return View();
        }

        var workouts = await _workoutService.GetByDateRange(from, to);
        return View("Index", workouts);
    }
    
}
