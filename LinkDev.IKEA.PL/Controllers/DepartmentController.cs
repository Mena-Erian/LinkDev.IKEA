using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance: DepartmentController is a Controller
    // Composition(not aggregation): DepartmentController has a IDepartmentService
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;

        //Used if i have just some action need to this service not all actions
        ///[FromServices]
        ///public IDepartmentService DepartmentService { get; set; }

        public DepartmentController(IDepartmentService departmentService) // Ask Runtime for Creating an Instance from 
        {
            _departmentService = departmentService;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
