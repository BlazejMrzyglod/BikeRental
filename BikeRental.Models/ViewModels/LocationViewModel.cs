using BikeRental.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.ViewModels
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }

        public LocationViewModel() { }
        public LocationViewModel(Location location)
        {
            Id = location.Id;
            Address = location.Address;
        }
    }
}
