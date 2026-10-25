using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;   // Admin | Restaurant | Employee

        // Links to Restaurant.Id or Employee.Id; null for Admin
        public int? LinkedId { get; set; }
    }

    // Lightweight model used only after successful login (no password exposed)
    public class LoggedInUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int? LinkedId { get; set; }
    }
}
