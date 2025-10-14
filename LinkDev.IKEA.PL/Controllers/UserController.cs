using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkDev.IKEA.PL.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager) : Controller
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

    }
}
