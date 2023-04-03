using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
