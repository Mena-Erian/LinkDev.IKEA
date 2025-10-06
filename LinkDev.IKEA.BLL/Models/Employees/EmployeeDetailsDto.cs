using LinkDev.IKEA.BLL.Models.Departments;
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
    public record EmployeeDetailsDto(
       EmployeeDto Employee,
       DepartmentDetailsDto? Department,
       int YearsOfExperience
        );
}
