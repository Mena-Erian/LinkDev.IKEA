using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    //public virtual Department? DepartmentMng { get; set; }
    //public virtual Department? Department { get; set; }
    public record EmployeeDto(
        int Id,
        string FirstName,
        string LastName,
        string? Email,
        string? PhoneNumber,
        string? Address,
        decimal Salary,
        bool IsActive,
        int Age,
        string? Image,
        DateOnly HireDate,
        Gender Gender,
        EmployeeType EmployeeType,
        int? DepartmentId,
        string? DepartmentMngName,
        string CreatedBy,
        DateTime CreatedOn,
        string LastModifiedBy, 
        DateTime LastModifiedOn)
    {
        public string FullName => $"{FirstName} {LastName}";
        public string FormattedHireDate => HireDate.ToString("MMMM d,yyy");
    };
}
