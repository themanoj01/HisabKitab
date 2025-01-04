using System.ComponentModel.DataAnnotations;

namespace HisabKitab.Model
{
    public class Users
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Preferred Currency is required")]
        public string Currency {  get; set; }
        public decimal Balance { get; set; }
    }
}
