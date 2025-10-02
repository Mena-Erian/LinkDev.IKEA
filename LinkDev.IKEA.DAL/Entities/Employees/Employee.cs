using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.Employees
{
    public class Employee : BaseAuditableEntity<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HireDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public string? Image { get; set; }

        public virtual Department? Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
