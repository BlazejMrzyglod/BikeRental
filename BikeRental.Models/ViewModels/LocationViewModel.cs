using BikeRental.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.ViewModels
{
    public class LocationViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
