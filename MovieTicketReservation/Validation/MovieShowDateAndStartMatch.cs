using MovieTicketReservation.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketReservation.Validation
{
    public class MovieShowDateAndStartMatch : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var movieShows = (MovieShows)validationContext.ObjectInstance;

            if (movieShows.Date.Date != movieShows.Start.Date)
            {
                return new ValidationResult("The date and start must match.");
            }
            if(movieShows.Start <= DateTime.Now)
            {
                return new ValidationResult("The date can't be in the past!");

            }

            return ValidationResult.Success;
        }
    }
}
