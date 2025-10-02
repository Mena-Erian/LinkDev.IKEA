using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public record UpdateEmployeeDto(
        int Id,
        string FirstName,
        string LastName,
        string? Email,
        string? PhoneNumber,
        string? Address,
        decimal Salary,
        bool IsActive,
        int? Age,
        string? Image,
        DateOnly HireDate,
        Gender Gender,
        EmployeeType EmployeeType,
        int? DepartmentId
        );
}
