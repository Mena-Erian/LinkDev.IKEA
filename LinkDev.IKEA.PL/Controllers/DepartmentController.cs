using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance: DepartmentController is a Controller
    // Composition(not aggregation): DepartmentController has a IDepartmentService
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

        [HttpGet] // GET: /Department/Details/id

        public IActionResult Details([FromRoute] int? id)
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

            return View(departmentDetailsViewModel);
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
