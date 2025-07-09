using FitTrackerApp.Domain.DomainModels;
using FitTrackerApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class ProgressEntriesController : Controller
{
    private readonly IProgressEntryService _progressEntryService;

    public ProgressEntriesController(IProgressEntryService progressEntryService)
    {
        _progressEntryService = progressEntryService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var progressEntries = await _progressEntryService.GetByUserIdAsync(userId);
        return View(progressEntries);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();

        var progressEntry = await _progressEntryService.GetById(id.Value);
        if (progressEntry == null) return NotFound();

        return View(progressEntry);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Date,WeightKg,BodyFatPercentage,UserId,Id")] ProgressEntry progressEntry)
    {
        if (ModelState.IsValid)
        {
            progressEntry.Id = Guid.NewGuid();
            progressEntry.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            await _progressEntryService.Insert(progressEntry);
            return RedirectToAction(nameof(Index));
        }

        return View(progressEntry);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var progressEntry = await _progressEntryService.GetById(id.Value);
        if (progressEntry == null) return NotFound();

        return View(progressEntry);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Date,WeightKg,BodyFatPercentage,UserId,Id")] ProgressEntry progressEntry)
    {
        if (id != progressEntry.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                if (string.IsNullOrEmpty(progressEntry.UserId))
                    progressEntry.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _progressEntryService.Update(progressEntry);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressEntryExists(progressEntry.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(progressEntry);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var progressEntry = await _progressEntryService.GetById(id.Value);
        if (progressEntry == null) return NotFound();

        return View(progressEntry);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var progressEntry = await _progressEntryService.GetById(id);
        if (progressEntry != null)
        {
            await _progressEntryService.DeleteById(progressEntry.Id);
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ProgressEntryExists(Guid id)
    {
        return _progressEntryService.GetById(id) != null;
    }

    public async Task<IActionResult> ByUser(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID cannot be null or empty.");
        }

        var progressEntries = await _progressEntryService.GetByUserIdAsync(userId);

        if (progressEntries == null || !progressEntries.Any())
        {
            return NotFound($"No progress entries found for user ID: {userId}");
        }

        ViewBag.UserId = userId;
        return View("Index", progressEntries);
    }

    [HttpGet]
    public async Task<IActionResult> ProgressSummary()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var progressSummary = await _progressEntryService.GetProgressSummary(userId);

        if (progressSummary.WeightDiff == 0 && progressSummary.BodyFatDiff == 0)
        {
            ViewBag.Message = "Not enough progress entries to calculate summary.";
           
            return View("ProgressSummary");
        }

        ViewBag.WeightDiff = progressSummary.WeightDiff;
        ViewBag.BodyFatDiff = progressSummary.BodyFatDiff;

        return View();
    }


}
