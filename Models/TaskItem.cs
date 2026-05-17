namespace mvc_project.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Title is mandatory")]
        public string Title { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; } = string.Empty;

        public string Priority { get; set; } = "Medium"; // Low, Medium, High
        public string Status { get; set; } = "Pending";  // Pending, In Progress, Done
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
