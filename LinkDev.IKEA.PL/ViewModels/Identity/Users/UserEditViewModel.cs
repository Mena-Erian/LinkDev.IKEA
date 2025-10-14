using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity.Users
{
    public class UserEditViewModel
    {
        public required string Id { get; init; }

        [Required(ErrorMessage = "First Name is Required")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public bool EmailIsConfirm { get; set; } 
        public bool TwoFactorEnabled { get; set; }
        public string? PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
