using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]   // GET: /EmployeeViewModel/Index
        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            var queryParameters = new QueryParameters()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };


            var employees = _employeeService.GetEmployees(queryParameters);

            var model = new EmployeeListViewModel()
            {
                Employees = employees.Data.Select(emp => new EmployeeViewModel()
                {
                    Id = emp.Id,
                    FullName = $"{emp.FirstName} {emp.LastName}",
                    Email = emp.Email ?? string.Empty,
                    PhoneNumber = emp.PhoneNumber ?? string.Empty,
                    Address = emp.Address ?? string.Empty,
                    Salary = emp.Salary,
                    IsActive = emp.IsActive,
                    Age = emp.Age ?? default,
                    FormattedHireDate = emp.HireDate.ToString(),
                    Gender = emp.Gender,
                    EmployeeType = emp.EmployeeType,
                    Department = emp.DepartmentId.ToString() ?? string.Empty,
                    CreatedBy = emp.CreatedBy,
                    CreatedOn = emp.CreatedOn,
                    LastModifiedBy = emp.LastModifiedBy,
                    LastModifiedOn = emp.LastModifiedOn
                }),
                Page = employees.PageIndex,
                PageSize = employees.PageSize,
                TotalCount = employees.TotalPageCount,
            };

            return View(model);
        }
    }
}

/// PageIndex = employees.PageIndex,
/// PageSize = employees.PageSize,
/// TotalCount = employees.TotalCount,