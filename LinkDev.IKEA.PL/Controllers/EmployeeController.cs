using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Index
        [HttpGet] // GET: /EmployeeViewModel/Index
        public IActionResult Index(
            string searchTerm = "",
            string sortBy = "",
            bool sortAscending = true,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var queryParameters = new QueryParameters()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortAscending = sortAscending
            };


            var paginatedResult = _employeeService.GetEmployees(queryParameters);

            var model = new EmployeeListViewModel()
            {
                Employees = paginatedResult.Data.Select(emp => new EmployeeViewModel()
                {
                    Id = emp.Id,
                    FullName = $"{emp.FirstName} {emp.LastName}",
                    Email = emp.Email ?? string.Empty,
                    PhoneNumber = emp.PhoneNumber ?? string.Empty,
                    Address = emp.Address ?? string.Empty,
                    Salary = emp.Salary,
                    IsActive = emp.IsActive,
                    Age = emp.Age,
                    FormattedHireDate = emp.HireDate.ToString(),
                    Gender = emp.Gender,
                    EmployeeType = emp.EmployeeType,
                    Department = emp.DepartmentId.ToString() ?? string.Empty,
                    CreatedBy = emp.CreatedBy,
                    CreatedOn = emp.CreatedOn,
                    LastModifiedBy = emp.LastModifiedBy,
                    LastModifiedOn = emp.LastModifiedOn
                }),
                Page = paginatedResult.PageIndex,
                PageSize = paginatedResult.PageSize,
                TotalCount = paginatedResult.TotalPageCount,
            };

            return View(model);
        }

        #endregion

        #region Details
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
                Age = employeeDetails.Employee.Age,
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
        #endregion

        #region Create

        [HttpGet] // GET: Employee/Create
        public IActionResult Create()
        {

            return View(new EmployeeCreateViewModel() { HiringDate = DateOnly.FromDateTime(DateTime.Now) });
        }

        [HttpPost] // POST: /Employee/Create
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

                message = $"An Error Occurred: {message}, Please Try Again Later";

            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update

        [HttpGet] // GET: /Employee/Edit
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee == null) return BadRequest();

            var viewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email ?? string.Empty,
                PhoneNumber = employee.PhoneNumber ?? string.Empty,
                Address = employee.Address ?? string.Empty,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Age = employee.Age,
                HiringDate = employee.HireDate,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                DepartmentId = employee.DepartmentId,
            };
            TempData["Id"] = id;
            return View(viewModel);
        }

        [HttpPost] // POST: /Employee/Edit/{id}
        public IActionResult Edit([FromRoute] int id, EmployeeEditViewModel employeeModel)
        {
            if ((int?)TempData["Id"] != id)
            {
                ModelState.AddModelError("Id", "Invalid Id");
                return View(employeeModel);
            }

            if (!ModelState.IsValid)
                return View(employeeModel);


            var message = "Employee Editing Successfully";

            try
            {
                var isUpdated = _employeeService.UpdateEmployee(new UpdateEmployeeDto(
                employeeModel.Id,
                employeeModel.FirstName,
                employeeModel.LastName,
                employeeModel.Email ?? null,
                employeeModel.PhoneNumber ?? null,
                employeeModel.Address ?? null,
                employeeModel.Salary,
                employeeModel.IsActive,
                employeeModel.Age,
                Image: default,
                employeeModel.HiringDate,
                employeeModel.Gender,
                employeeModel.EmployeeType,
                employeeModel.DepartmentId ?? null
                )) > 0;

                if (!isUpdated)
                    message = "Employee Editing Failed";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());

                message = $"An Error Occurred: {message}, Please Try Again Later";
            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost] // Post: /Employee/Delete/{id} 
        public IActionResult Delete(int id)
        {
            var message = "Employee Deleted Successfully";
            try
            {
                if (!_employeeService.DeleteEmployee(id))
                {
                    message = "Failed to Deleted Employee";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                if (_webHostEnvironment.IsDevelopment())
                {
                    message = $"An Error Occurred: {message}, Please Try Again Later";
                }
                message = $"An Error Occurred, Please Try Again Later";

            }


            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}

/// PageIndex = paginatedResult.PageIndex,
/// PageSize = paginatedResult.PageSize,
/// TotalCount = paginatedResult.TotalCount,