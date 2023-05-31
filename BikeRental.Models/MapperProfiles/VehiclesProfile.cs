using BikeRental.Models.Models;
using BikeRental.Models.ViewModels;
using AutoMapper;

namespace BikeRental.Models.MapperProfiles
{
    public class VehiclesProfile : Profile
    {
        public VehiclesProfile()
        {
            CreateMap<Vehicle, VehicleDetailViewModel>().ForMember(dest => dest.Location, opt => opt.MapFrom(x => x.Location.Name)).ForMember(dest => dest.Type, opt => opt.MapFrom(x => x.Type.Name));
            CreateMap<VehicleDetailViewModel, Vehicle>().ForMember(dest => dest.Location, opt => opt.Ignore())
                                                        .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<Vehicle, VehicleItemViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Manufacturer + ' ' + x.Model));
        }
    }
}
