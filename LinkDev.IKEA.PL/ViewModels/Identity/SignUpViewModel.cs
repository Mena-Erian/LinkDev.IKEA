using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public required string  FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The Password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }


        public bool IsAgree { get; set; }
    }
}
