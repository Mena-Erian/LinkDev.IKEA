using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity.Users
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="Email Can Not Be Empty!")]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
