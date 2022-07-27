using System.ComponentModel.DataAnnotations;

namespace Product_Management_API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
