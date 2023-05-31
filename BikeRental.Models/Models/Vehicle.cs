using BikeRental.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BikeRental.Models.Models
{
    public class Vehicle : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int Price { get; set; }
        public Guid LocationId { get; set; }
        public virtual Location? Location { get; set; }
        public bool Availability { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public Guid TypeId { get; set; }
        public virtual VehicleType? Type { get; set; }
        public Reservation? Reservation { get; set; }

    }
}
