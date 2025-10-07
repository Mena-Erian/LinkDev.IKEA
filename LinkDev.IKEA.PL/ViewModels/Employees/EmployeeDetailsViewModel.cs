using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Departments;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeDetailsViewModel
    {

        public int Id { get; set; }

        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HireDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public string? Image { get; set; }
        public string? DepartmentMngName { get; set; }

        public DepartmentDetailsDto? Department { get; set; }
        public int? DepartmentId { get; set; }


        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public required string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
