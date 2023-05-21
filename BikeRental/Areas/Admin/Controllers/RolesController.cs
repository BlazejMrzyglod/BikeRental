using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeRental.Models.Models;
using AutoMapper;
using BikeRental.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BikeRental.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
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
            List<RoleViewModel> usersWithRoles = new();
            foreach (var user in users)
            {
                usersWithRoles.Add(_mapper.Map<RoleViewModel>(user));
            }
            return View(usersWithRoles);
        }
        //POST: RolesController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("UserName, Role")] RoleViewModel user)
        {
                var result = _userManager.FindByNameAsync(user.UserName).Result;
                var role = _userManager.GetRolesAsync(result).Result.FirstOrDefault();
                if (role != user.Role)
                {
                    _userManager.RemoveFromRoleAsync(result, role);
                    _userManager.AddToRoleAsync(result, user.Role);
                }
            
            return RedirectToAction(nameof(Index));
        }


    }
}
