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
        EmployeeDto? GetEmployeeById(int employeeId);

        EmployeeDetailsDto? GetEmployeeDetailsById(int employeeId);
        
        IEnumerable<EmployeeDto> GetEmployees();

        PaginatedResult<EmployeeDto> GetEmployees(QueryParameters queryParameters);

        int CreateEmployee(CreateEmployeeDto employee);

        int UpdateEmployee(UpdateEmployeeDto employee);

        bool DeleteEmployee(int employeeId);
    }
}
