using BikeRental.Models.ViewModels;
using FluentValidation;

namespace BikeRental.Validation
{
    public class ReservationValidator : AbstractValidator<ReservationViewModel>
    {
        public ReservationValidator() 
        {
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(x => x.ReservationDate).LessThan(x => x.EndDate);
        }
    }
}
