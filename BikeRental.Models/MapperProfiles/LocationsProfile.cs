using AutoMapper;
using BikeRental.Models.Models;
using BikeRental.Models.ViewModels;

namespace BikeRental.Models.MapperProfiles
{
    public class LocationsProfile : Profile
    {
        public LocationsProfile()
        {
            CreateMap<Location, LocationViewModel>();
            CreateMap<LocationViewModel, Location>().ForMember(dest => dest.Vehicles, opt => opt.Ignore());

        }
    }
}
