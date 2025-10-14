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
        Task<IEnumerable<DepartmentResponseDto>> GetDepartmentsAsync();
        Task<DepartmentDetailsDto?> GetDepartmentsByIdAsync(int departmentId);
        Task<int> CreateDepartmentAsync(CreateDepartmentDto department);
        Task<int> UpdateDepartmentAsync(UpdateDepartmentDto department);
        Task<bool> DeleteDepartmentAsync(int departmentId);
    }
}
