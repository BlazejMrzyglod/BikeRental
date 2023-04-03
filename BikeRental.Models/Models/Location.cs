using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.Models
{
    public class Location : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
