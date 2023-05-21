using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeRental.Models.Models;
using AutoMapper;
using BikeRental.Models.ViewModels;

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

    }
}
