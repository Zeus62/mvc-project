using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Users
        public IActionResult Index()
        {
            var users = _context.Users.OrderBy(u => u.Username).ToList();
            return View(users);
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                ModelState.AddModelError("Password", "Password is required");
            }

            if (!ModelState.IsValid)
                return View(user);

            if (_context.Users.Any(u => u.Username == user.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken");
                return View(user);
            }

            // Force all new users created via this form to be standard Users
            user.Role = "User";

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Users/Edit/5
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AppUser updatedUser)
        {
            if (!ModelState.IsValid)
                return View(updatedUser);

            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            // Check if changing username to one that already exists
            if (user.Username != updatedUser.Username && _context.Users.Any(u => u.Username == updatedUser.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken");
                return View(updatedUser);
            }

            user.Username = updatedUser.Username;
            
            // Only update password if a new one was provided
            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
            {
                user.Password = updatedUser.Password;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Users/Delete/5
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return Json(new { success = false, message = "User not found" });

            if (user.Username == User.Identity?.Name)
                return Json(new { success = false, message = "You cannot delete your own account" });

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Json(new { success = true, message = "User deleted successfully" });
        }
    }
}
