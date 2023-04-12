using BikeRental.Models.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator() 
        {
            RuleFor(x => x.ReservationDate).GreaterThanOrEqualTo(DateTime.UtcNow);
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(x => x.ReservationDate).LessThan(x => x.EndDate);
        }
    }
}
