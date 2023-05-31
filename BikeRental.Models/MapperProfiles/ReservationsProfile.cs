using AutoMapper;
using BikeRental.Models.Models;
using BikeRental.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.MapperProfiles
{
    public class ReservationsProfile : Profile
    {
        public ReservationsProfile() 
        {
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<ReservationViewModel, Reservation>().ForMember(dest => dest.Vehicle, opt => opt.Ignore());
        }
    }
}
