namespace mvc_project.Models
{
    public static class DataStore
    {
        private static int _nextUserId = 3;
        private static int _nextTaskId = 4;

        public static List<AppUser> Users { get; } = new List<AppUser>
        {
            new AppUser { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
            new AppUser { Id = 2, Username = "user", Password = "user123", Role = "User" }
        };

        public static List<TaskItem> Tasks { get; } = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Design Homepage", Description = "Create the main landing page layout", Priority = "High", Status = "In Progress", CreatedBy = "admin", CreatedAt = DateTime.Now.AddDays(-2) },
            new TaskItem { Id = 2, Title = "Fix Login Bug", Description = "Resolve the authentication timeout issue", Priority = "High", Status = "Pending", CreatedBy = "admin", CreatedAt = DateTime.Now.AddDays(-1) },
            new TaskItem { Id = 3, Title = "Write Documentation", Description = "Document the API endpoints", Priority = "Low", Status = "Done", CreatedBy = "user", CreatedAt = DateTime.Now }
        };

        public static int GetNextUserId() => _nextUserId++;
        public static int GetNextTaskId() => _nextTaskId++;
    }
}
