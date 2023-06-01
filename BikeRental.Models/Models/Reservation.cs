using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models.Models
{
	public enum Status
	{
		Realizacja,
		Wypożyczone,
		Zwrócone
	}
	public class Reservation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
		public Status Status { get; set; }
		public string? User { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}

