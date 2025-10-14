using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity.Users
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
