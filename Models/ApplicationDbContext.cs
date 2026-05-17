using Microsoft.EntityFrameworkCore;

namespace mvc_project.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<TaskItem> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Users
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
                new AppUser { Id = 2, Username = "user", Password = "user123", Role = "User" }
            );

            // Seed Tasks
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "Design Homepage", Description = "Create the main landing page layout", Priority = "High", Status = "In Progress", CreatedBy = "admin", CreatedAt = DateTime.Now.AddDays(-2) },
                new TaskItem { Id = 2, Title = "Fix Login Bug", Description = "Resolve the authentication timeout issue", Priority = "High", Status = "Pending", CreatedBy = "admin", CreatedAt = DateTime.Now.AddDays(-1) },
                new TaskItem { Id = 3, Title = "Write Documentation", Description = "Document the API endpoints", Priority = "Low", Status = "Done", CreatedBy = "user", CreatedAt = DateTime.Now }
            );
        }
    }
}
