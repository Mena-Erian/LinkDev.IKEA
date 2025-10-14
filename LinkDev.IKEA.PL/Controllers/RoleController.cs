using LinkDev.IKEA.PL.ViewModels.Identity.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace LinkDev.IKEA.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController(RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _webHostEnvironment) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var rolesQuery = _roleManager.Roles.AsQueryable();

            var roles = rolesQuery.Select(role => new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name ?? ""
            }).ToList();

            return View(roles);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRoleViewModel model)
        {
            var message = $"{model.Name} Role Created Successfully";

            try
            {
                if (model is null)
                {
                    message = "Model Value Is Null";
                    throw new ArgumentNullException(message);
                }


                if (!ModelState.IsValid)
                {
                    message = "Model Value Is Not Valid";
                    throw new ArgumentNullException(message);
                }

                var result = _roleManager.CreateAsync(new IdentityRole()
                {
                    Id = model.Id,
                    Name = model.Name,
                }).Result;

                if (result.Succeeded)
                {
                    TempData["Message"] = message;
                    return RedirectToAction(nameof(Index));
                }
                message = "Result Of Operation is not Success";
                throw new Exception(message);
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    message = ex.Message;
                    //TempData["Message"] = message;
                    ModelState.AddModelError("", message);
                }
                else
                {
                    message = "Invalid Operation";
                    //TempData["Message"] = message;
                    ModelState.AddModelError("", message);
                }
            }

            return View(model);
        }

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id is null) return BadRequest();

            var role = _roleManager.FindByIdAsync(id).Result;

            if (role is null) return BadRequest();

            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role?.Name ?? ""
            };

            return View(roleViewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var role = _roleManager.FindByIdAsync(id).Result;

            if (role is null) return NotFound();

            var roleViewModel = new RoleViewModel()
            {
                Id = id,
                Name = role.Name ?? ""
            };

            TempData["roleId"] = roleViewModel.Id;
            return View(roleViewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoleViewModel model)
        {
            string message = "role Updated Successfully";
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("ModelState Is Not Valid");

                if (((TempData["roleId"] ?? "").ToString()) != model.Id)
                    throw new Exception("role Id Not Valid");

                var role = _roleManager.FindByIdAsync(model.Id).Result;
                if (role is null)
                    throw new Exception("role not found by Id");

                if (role.Name == "Admin")
                    throw new Exception("Can't Edit Or Change In Admin Name");
                

                role.Name = model.Name;


                var result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded)
                {
                    TempData["Message"] = message;
                    return RedirectToAction(nameof(Index));
                }
                else
                    message = "role Can Not Be Updated";
                throw new Exception(message);
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    message = ex.Message;
                    TempData["Message"] = message;
                    ModelState.AddModelError("", message);
                }
                else
                {
                    message = "Invalid Operation";
                    TempData["Message"] = message;
                    ModelState.AddModelError("", message);
                }
            }
            return View(model);

        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(string id)
        {

            string message = "role Deleted Successfully";
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;

                if (role is null)
                    throw new Exception("role not found by Id");

                var result = _roleManager.DeleteAsync(role).Result;

                if (!result.Succeeded)
                {
                    message = "role Can Not Be Deleted";
                    throw new Exception(message);
                }

                TempData["Message"] = message;
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    message = ex.Message;
                    TempData["Message"] = message;
                    ModelState.AddModelError("", message);
                }
                else
                {
                    message = "Invalid Operation";
                    TempData["Message"] = message;
                    ModelState.AddModelError("", message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }

}
