using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Sign UP
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ApplicationUser? user = await _userManager.FindByNameAsync(model.UserName);

            if (user is not null)
            {
                ModelState.AddModelError("UserName", "This username is already taken");
                return View(model);
            }

            user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.IsAgree,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(SignIn));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        #endregion

        #region Sign In
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }

            // Check Password
            var flag = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!flag)
            {
                ModelState.AddModelError("", "Invalid Password attempt");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.IsNotAllowed)
                ModelState.AddModelError("", "Your Account is not Confirmed yet!");
            if (result.IsNotAllowed)
                ModelState.AddModelError("", $"Your Account is Locked out {user.LockoutEnd}");
            /// if (result.RequiresTwoFactor)
            /// {
            /// }

            if (result.Succeeded)
                return RedirectToAction(nameof(EmployeeController.Index));

            return View(model);
        }

        #endregion



    }
}
