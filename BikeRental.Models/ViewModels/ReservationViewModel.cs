using BikeRental.Models.Models;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.ViewModels
{
    public class ReservationViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [DisplayName("Data rezerwacji")]
        public DateTime? ReservationDate { get; set; }
        [Required]
        [DisplayName("Data początkowa")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("Data końcowa")]
        public DateTime EndDate { get; set; }
        [Required]
        public string? User { get; set; }
        public bool? IsActive { get; set; }
        [Required]
        public Guid? VehicleId { get; set; }
    }
}
