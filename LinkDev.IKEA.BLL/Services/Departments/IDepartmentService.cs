using LinkDev.IKEA.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentResponseDto> GetDepartments();

        DepartmentDetailsDto? GetDepartmentsById(int departmentId);

        int CreateDepartment(CreateDepartmentDto department);

        int UpdateDepartment(UpdateDepartmentDto department);

        bool DeleteDepartment(int departmentId);
    }
}
