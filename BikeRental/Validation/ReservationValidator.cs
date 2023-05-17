using BikeRental.Models.Models;
using FluentValidation;

namespace BikeRental.Validation
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
