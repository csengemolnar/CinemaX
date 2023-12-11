using MovieTicketReservation.Areas.Identity.Data;
using MovieTicketReservation.Models;
using MovieTicketReservation.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketReservation.Validation
{
    public class IsHallEmptyAtTheWantedDate: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var movieShows = (MovieShows)validationContext.ObjectInstance;
            var dbContext = (MovieTicketReservationContext)validationContext.GetService(typeof(MovieTicketReservationContext));

            
            var movieLength = dbContext.Movies
                .Where(m => m.MovieId == movieShows.MovieId)
                .Select(m => m.MovieLength)
                .FirstOrDefault();

            bool isHallAvailable = CheckHallAvailability(dbContext, movieShows.HallId, movieShows.Start, movieLength);

            if (!isHallAvailable)
            {
                return new ValidationResult("The hall is not available for the selected date and time.");
            }

            return ValidationResult.Success;
        }

        private bool CheckHallAvailability(MovieTicketReservationContext? moviecontext, int hallId, DateTime start, int movieLength)
        {
            int bufferTime = 30;
            TimeSpan movieDuration = TimeSpan.FromMinutes(movieLength + bufferTime);
            DateTime end = start +movieDuration;

            
            var screeningsInHall = moviecontext.MovieShows
                .Where(ms => ms.HallId == hallId)
                .ToList(); 

            
            var overlappingScreenings = screeningsInHall
                .Where(ms => ms.Start < end && (ms.Start + movieDuration) > start)
                .ToList();

            return !overlappingScreenings.Any();
        }
    }
}

