using LinkDev.IKEA.DAL.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(3, ErrorMessage = "Min Length of First Name is 3 Chars")]
        [MaxLength(50, ErrorMessage = "Max Length of First Name is 50 Chars")]
        public string FirstName { get; set; } = default!;

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(3, ErrorMessage = "Min Length of Last Name is 3 Chars")]
        [MaxLength(50, ErrorMessage = "Max Length of Last Name is 50 Chars")]
        public string LastName { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 1000000, ErrorMessage = "Salary must be greater than 0 and less than 1,000,000")]
        public decimal Salary { get; set; }

        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                            , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Department_")]
        public int? DepartmentId { get; set; }

        // For the DropDown in the view
        //public IEnumerable<SelectListItem> Departments { get; set; } = new HashSet<SelectListItem>();
        public IEnumerable<SelectListItem>? Departments { get; set; }
    }
}
