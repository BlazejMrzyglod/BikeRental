using AutoMapper;
using BikeRental.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.MapperProfiles
{
    public class RoleResolver : IValueResolver<IdentityUser, RoleViewModel, string>
    {
        private UserManager<IdentityUser> _userManager;
        public RoleResolver(UserManager<IdentityUser> userManager) 
        {
            _userManager = userManager;
        }
        public string Resolve(IdentityUser user, RoleViewModel role, string member, ResolutionContext context)
        {
            return _userManager.GetRolesAsync(user).Result.FirstOrDefault();
        }
    }
}
