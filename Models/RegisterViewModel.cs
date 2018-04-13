using System.ComponentModel.DataAnnotations;
namespace wedding_planner.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirmation is required")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }
    }
}