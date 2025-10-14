using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance: DepartmentController is a Controller
    // Composition(not aggregation): DepartmentController has a IDepartmentService
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Services
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentService _departmentService;



        //Used if i have just some action need to this service not all actions
        ///[FromServices]
        ///public IDepartmentService DepartmentService { get; set; }

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService) // Ask Runtime for Creating an Instance from 
        {
            _logger = logger;
            _departmentService = departmentService;
        }
        #endregion

        #region Index
        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments();

            return View(departments.Select(d => new DepartmentViewModel()
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                CreationDate = d.UpdatedDate
            }));
        }
        #endregion

        #region Details

        [HttpGet] // GET: /Department/Details/_id
        public IActionResult Details([FromRoute] int? id, string viewName = "Details")
        {

            if (!id.HasValue) return BadRequest(); //400

            var department = _departmentService.GetDepartmentsById(id.Value);

            if (department == null) return NotFound(); //404

            var departmentDetailsViewModel = new DepartmentDetailsViewModel()
            {
                Id = department.Id,

                Code = department.Code,
                Name = department.Name,
                Description = department.Description ?? string.Empty,
                CreationDate = department.CreationDate,

                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn,
            };

            return View(viewName, departmentDetailsViewModel);
        }

        #endregion

        #region Create

        [HttpGet] // GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // POST: /Department/Create
        public IActionResult Create(CreateDepartmentViewModel model)
        {
            var message = string.Empty;
            try
            {
                if (!ModelState.IsValid) // Server-Side Validation
                    return View(model);

                message = $"{model.Name} Department Created Successfully";

                var departmentToCreate = new CreateDepartmentDto(model.Code, model.Name, model.Description, DateOnly.FromDateTime(model.CreationDate));
                var created = _departmentService.CreateDepartment(departmentToCreate) > 0;

                if (!created) message = "Failed to Create Department";
            }
            catch (Exception ex)
            {
                // 1. Log Exception in Database Or External file (by SerialLog Package)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());

                // 2. Set Message
                message = "An Error Occurred, Please Try Again Later";
            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update
        [HttpGet] // GET: /Department/Edit/_id?
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest(); // 400

            var department = _departmentService.GetDepartmentsById(id.Value);
            if (department is null) return BadRequest();

            var departmentViewModel = new UpdateDepartmentViewModel()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            };

            TempData["DepartmentUpdateId"] = departmentViewModel.Id;

            return View(departmentViewModel);
        }

        [HttpPost] // POST: /Department/Edit
        public IActionResult Edit([FromRoute] int id, UpdateDepartmentViewModel model)
        {

            if ((int?)TempData["DepartmentUpdateId"] != id)
            {
                ModelState.AddModelError("Id", "Invalid Id");
                return View(model); // 400
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Id", "Invalid Id");
                return View(model); // 400
            }
            string message = string.Empty;
            try
            {
                var departmentToUpdate = new UpdateDepartmentDto(id, model.Name, model.Code, model.Description, model.CreationDate);

                var IsUpdated = _departmentService.UpdateDepartment(departmentToUpdate) > 0;
                if (!IsUpdated)
                    message = $"Failed to Update {model.Name} Department";

                message = $"{model.Name} Department Updated Successfully";
            }
            catch (Exception ex)
            {
                // Best Practice is make middle ware
                // 1. Log Exception in Database Or External file (by SerialLog Packege)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());

                // 2. Set Message
                message = "An Error Occurred, Please Try Again Later";
            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));


        }
        #endregion

        #region Delete
        // [HttpGet] //Get: /Department/Delete/_id
        // public IActionResult Delete(int? _id)
        // {
        //     return RedirectToAction(nameof(Details), new { _id, viewName = "Delete" });
        // }

        [HttpPost] //Post: /Department/Delete/_id
        public IActionResult Delete(int id)
        {

            string message = string.Empty;
            try
            {
                var IsDeleted = _departmentService.DeleteDepartment(id);
                if (!IsDeleted)
                    message = $"Failed to Deleted Department";

                message = $"Department Deleted Successfully";
            }
            catch (Exception ex)
            {
                // Best Practice is make middle ware
                // 1. Log Exception in Database Or External file (by SerialLog Packege)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());

                // 2. Set Message
                message = "An Error Occurred, Please Try Again Later";
            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
