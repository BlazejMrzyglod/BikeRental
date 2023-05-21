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
		
		public RolesProfile()
		{
			CreateMap<IdentityUser, RoleViewModel>().ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
													.ForMember(dest => dest.Role, opt => opt.MapFrom<RoleResolver>())
													.ForMember(dest => dest.SelectListRoles, opt => opt.MapFrom<RolesResolver>());
		}
	}

	
}
