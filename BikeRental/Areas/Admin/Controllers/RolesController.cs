using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public RolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: RolesController
        public ActionResult Index()
        {
            var users = _userManager.Users;
            var usersWithRoles = users.Select(user => new
            {
                UserName = user.UserName,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()
            });
            return View(usersWithRoles);
        }

    }
}
