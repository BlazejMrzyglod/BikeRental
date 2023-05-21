using AutoMapper;
using BikeRental.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public class RolesResolver : IValueResolver<IdentityUser, RoleViewModel, SelectList>
    {
        private RoleManager<IdentityRole> _roleManager;
        public RolesResolver(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public SelectList Resolve(IdentityUser user, RoleViewModel role, SelectList member, ResolutionContext context)
        {
            SelectList selectList = new SelectList(_roleManager.Roles);
            return selectList;
        }
    }
}
