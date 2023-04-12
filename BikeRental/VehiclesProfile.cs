using BikeRental.Models.Models;
using BikeRental.Models.ViewModels;
using AutoMapper;

namespace BikeRental
{
    public class VehiclesProfile : Profile
    {
        public VehiclesProfile()
        {
            CreateMap<Vehicle, VehicleDetailViewModel>().ForMember(dest => dest.Location, opt => opt.MapFrom(x => x.Location.Name)).ForMember(dest => dest.Type, opt => opt.MapFrom(x => x.Type.Name));
            CreateMap<VehicleDetailViewModel, Vehicle>().ForMember(dest => dest.Location, opt => opt.Ignore()).ForMember(dest => dest.LocationId, opt => opt.Ignore()).ForMember(dest => dest.TypeId, opt => opt.Ignore()).ForMember(dest => dest.Type, opt => opt.Ignore()).ForMember(dest => dest.ReservationId, opt => opt.Ignore()).ForMember(dest => dest.Reservation, opt => opt.Ignore());
            CreateMap<Vehicle, VehicleItemViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Manufacturer + ' ' + x.Model));
        }
    }
}
