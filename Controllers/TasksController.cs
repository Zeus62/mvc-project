using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Tasks
        public IActionResult Index()
        {
            var username = User.Identity?.Name;
            var isAdmin = User.IsInRole("Admin");

            List<TaskItem> tasks;
            if (isAdmin)
            {
                // Admin sees all tasks
                tasks = _context.Tasks.OrderByDescending(t => t.CreatedAt).ToList();
            }
            else
            {
                // Regular user sees only their own tasks
                tasks = _context.Tasks
                    .Where(t => t.CreatedBy == username)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToList();
            }

            ViewBag.IsAdmin = isAdmin;
            return View(tasks);
        }

        // GET: /Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem task)
        {
            // Remove validation for fields we set manually
            ModelState.Remove("CreatedBy");

            if (!ModelState.IsValid)
                return View(task);

            task.CreatedBy = User.Identity?.Name ?? "unknown";
            task.CreatedAt = DateTime.Now;

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Tasks/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, TaskItem updatedTask)
        {
            ModelState.Remove("CreatedBy");

            if (!ModelState.IsValid)
                return View(updatedTask);

            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.Status = updatedTask.Status;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: /Tasks/Delete/5  (AJAX endpoint)
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return Json(new { success = false, message = "Task not found" });

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Json(new { success = true, message = "Task deleted successfully" });
        }
    }
}
