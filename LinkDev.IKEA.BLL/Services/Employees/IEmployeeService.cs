using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId);

        Task<EmployeeDetailsDto?> GetEmployeeDetailsByIdAsync(int employeeId);

        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();

        Task<PaginatedResult<EmployeeDto>> GetEmployeesAsync(QueryParameters queryParameters);

        Task<int> CreateEmployeeAsync(CreateEmployeeDto employee);

        Task<int> UpdateEmployeeAsync(UpdateEmployeeDto employee);

        Task<bool> ChangeEmployeeStatusAsync(int id, bool isActive);

        Task<bool> DeleteEmployeeAsync(int employeeId);
    }
}
