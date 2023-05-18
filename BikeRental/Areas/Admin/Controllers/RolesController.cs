using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace BikeRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        public RolesController(UserManager<IdentityUser> userManager) 
        {
            _userManager = userManager;
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
