using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        //private readonly IDepartmentService _departmentService;
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

        [HttpGet] // GET: Employee/Details/{Id}
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest("Needs To Id");

            var employeeDetails = _employeeService.GetEmployeeDetailsById(id.Value);

            if (employeeDetails == null) return NotFound("Employee Id Not Found");

            var model = new EmployeeDetailsViewModel()
            {
                Id = employeeDetails.Employee.Id,
                FirstName = employeeDetails.Employee.FirstName,
                LastName = employeeDetails.Employee.LastName,
                Email = employeeDetails.Employee.Email ?? string.Empty,
                PhoneNumber = employeeDetails.Employee.PhoneNumber ?? string.Empty,
                Address = employeeDetails.Employee.Address ?? string.Empty,
                Salary = employeeDetails.Employee.Salary,
                IsActive = employeeDetails.Employee.IsActive,
                Age = employeeDetails.Employee.Age ?? default,
                HireDate = employeeDetails.Employee.HireDate,
                Gender = employeeDetails.Employee.Gender,
                EmployeeType = employeeDetails.Employee.EmployeeType,
                Department = employeeDetails.Department,
                CreatedBy = employeeDetails.Employee.CreatedBy,
                CreatedOn = employeeDetails.Employee.CreatedOn,
                LastModifiedBy = employeeDetails.Employee.LastModifiedBy,
                LastModifiedOn = employeeDetails.Employee.LastModifiedOn,
                DepartmentMngName = employeeDetails.Employee.DepartmentMngName
            };

            return View(model);
        }

        [HttpGet] // GET: Employee/Create
        public IActionResult Create()
        {

            return View(new EmployeeCreateViewModel() { HiringDate = DateOnly.FromDateTime(DateTime.Now) });
        }

        [HttpPost] //POST: /Employee/Create
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var message = "Employee Created Successfully";

            try
            {
                var isCreated = _employeeService.CreateEmployee(new CreateEmployeeDto(
                                                      model.Id,
                                                      model.FirstName,
                                                      model.LastName,
                                                      model.Email ?? null,
                                                      model.PhoneNumber ?? null,
                                                      model.Address ?? null,
                                                      model.Salary,
                                                      model.IsActive,
                                                      model.Age,
                                                      Image: default,
                                                      model.HiringDate,
                                                      model.Gender,
                                                      model.EmployeeType,
                                                      model.DepartmentId ?? null
                                                      )) > 0;

                if (!isCreated)
                    message = "Employee Creation Failed";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());

                message = "An Error Occurred, Please Try Again Later";

            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}

/// PageIndex = employees.PageIndex,
/// PageSize = employees.PageSize,
/// TotalCount = employees.TotalCount,