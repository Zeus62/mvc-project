namespace mvc_project.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Username is required")]
        [System.ComponentModel.DataAnnotations.StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters")]
        public string Username { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // "Admin" or "User"
    }
}
