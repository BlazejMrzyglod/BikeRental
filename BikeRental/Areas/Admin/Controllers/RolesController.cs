using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeRental.Models.Models;
using AutoMapper;
using BikeRental.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BikeRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        public RolesController(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: RolesController
        public ActionResult Index()
        {
            var users = _userManager.Users;
            List<RoleViewModel> usersWithRoles = new List<RoleViewModel>();
            foreach (var user in users)
            {
                usersWithRoles.Add(_mapper.Map<RoleViewModel>(user));
            }
            return View(usersWithRoles);
        }
        //POST: RolesController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("userName, role")] RoleViewModel user)
        {
                var result = _userManager.FindByNameAsync(user.userName).Result;
                var role = _userManager.GetRolesAsync(result).Result.FirstOrDefault();
                if (role != user.role)
                {
                    _userManager.RemoveFromRoleAsync(result, role);
                    _userManager.AddToRoleAsync(result, user.role);
                }
            
            return RedirectToAction(nameof(Index));
        }


    }
}
