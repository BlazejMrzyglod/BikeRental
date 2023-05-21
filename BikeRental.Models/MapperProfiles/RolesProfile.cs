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
	public class RolesProfile : Profile
	{
		private UserManager<IdentityUser> _userManager;
		public RolesProfile(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
			CreateMap<IdentityUser, RoleViewModel>().ForMember(dest => dest.userName, opt => opt.MapFrom(x => x.UserName))
													.ForMember(dest => dest.role, opt => opt.MapFrom(x => _userManager.GetRolesAsync(x).Result.FirstOrDefault()));

		}
	}
}
