using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkDev.IKEA.PL.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager, IWebHostEnvironment _webHostEnvironment) : Controller
    {
        // Services ---> Services [UserManager]
        // Index , Details , Edit , Delete , [Create user -> register]
        // 
        #region Index
        [HttpGet]
        public IActionResult Index(string searchValue)
        {

            var usersQuery = _userManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
                usersQuery = usersQuery.Where(u => !string.IsNullOrEmpty(u.Email) && u.Email.ToLower().Contains(searchValue));

            var users = usersQuery.Select(u => new UserViewModel()
            {
                Id = u.Id,
                Email = u.Email ?? "",
                FirstName = u.FirstName,
                LastName = u.LastName,
                //Roles.for _userManager.GetRolesAsync(u).Result.ToList()

            }).ToList();

            foreach (var item in users)
                item.Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(item.Id).Result!).GetAwaiter().GetResult();

            return View(users);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id is null) return BadRequest();

            var user = _userManager.FindByIdAsync(id).Result;

            if (user is null) return BadRequest();

            var userViewModel = new UserDetailsViewModel()
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                Roles = _userManager.GetRolesAsync(user).Result
            };



            return View(userViewModel);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = _userManager.FindByIdAsync(id).Result;

            if (user is null) return NotFound();

            var userViewModel = new UserEditViewModel()
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                Roles = _userManager.GetRolesAsync(user).Result,
                EmailIsConfirm = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                TwoFactorEnabled = user.TwoFactorEnabled,
            };
            //ViewBag.UserId = userViewModel.Id;
            TempData["UserId"] = userViewModel.Id;
            return View(userViewModel);
        }
        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            string message = "User Updated Successfully";
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("ModelState Is Not Valid");

                if (((TempData["UserId"] ?? "").ToString()) != model.Id)
                    throw new Exception("User Id Not Valid");

                var user = _userManager.FindByIdAsync(model.Id).Result;
                if (user is null)
                    throw new Exception("User not found by Id");

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailIsConfirm;
                user.PhoneNumber = model.PhoneNumber;
                user.TwoFactorEnabled = model.TwoFactorEnabled;
                user.UserName = model.UserName;

                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    TempData["Message"] = message;
                    return RedirectToAction(nameof(Index));
                }
                else
                    message = "User Can Not Be Updated";
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

    }
}
