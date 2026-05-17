using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                    ? DataStore.Tasks.Count
                    : DataStore.Tasks.Count(t => t.CreatedBy == username);
                ViewBag.PendingTasks = isAdmin
                    ? DataStore.Tasks.Count(t => t.Status == "Pending")
                    : DataStore.Tasks.Count(t => t.CreatedBy == username && t.Status == "Pending");
                ViewBag.CompletedTasks = isAdmin
                    ? DataStore.Tasks.Count(t => t.Status == "Done")
                    : DataStore.Tasks.Count(t => t.CreatedBy == username && t.Status == "Done");
                ViewBag.InProgressTasks = isAdmin
                    ? DataStore.Tasks.Count(t => t.Status == "In Progress")
                    : DataStore.Tasks.Count(t => t.CreatedBy == username && t.Status == "In Progress");
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
