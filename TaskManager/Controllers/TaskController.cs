using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Controllers;

public class TaskController : Controller
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<IActionResult> Index(string sortBy = "")
    {
        var tasks = await _taskService.GetSortedAsync(sortBy);
        ViewBag.CurrentSort = sortBy;
        return View(tasks);
    }

    public async Task<IActionResult> Details(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null) return NotFound();
        return View(task);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            ModelState.AddModelError("Title", "Title boş ola bilməz");

        if (task.Deadline.HasValue && task.Deadline.Value < DateTime.Now)
            ModelState.AddModelError("Deadline", "Deadline keçmiş tarix ola bilməz");

        if (!ModelState.IsValid)
            return View(task);

        await _taskService.AddAsync(task);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null) return NotFound();
        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            ModelState.AddModelError("Title", "Title boş ola bilməz");

        if (task.Deadline.HasValue && task.Deadline.Value < DateTime.Now)
            ModelState.AddModelError("Deadline", "Deadline keçmiş tarix ola bilməz");

        if (!ModelState.IsValid)
            return View(task);

        await _taskService.UpdateAsync(task);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}