using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var isAdmin = User.IsInRole("Admin");

                ViewBag.Username = username;
                ViewBag.IsAdmin = isAdmin;
                ViewBag.TotalTasks = isAdmin
                    ? _context.Tasks.Count()
                    : _context.Tasks.Count(t => t.CreatedBy == username);
                ViewBag.PendingTasks = isAdmin
                    ? _context.Tasks.Count(t => t.Status == "Pending")
                    : _context.Tasks.Count(t => t.CreatedBy == username && t.Status == "Pending");
                ViewBag.CompletedTasks = isAdmin
                    ? _context.Tasks.Count(t => t.Status == "Done")
                    : _context.Tasks.Count(t => t.CreatedBy == username && t.Status == "Done");
                ViewBag.InProgressTasks = isAdmin
                    ? _context.Tasks.Count(t => t.Status == "In Progress")
                    : _context.Tasks.Count(t => t.CreatedBy == username && t.Status == "In Progress");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
