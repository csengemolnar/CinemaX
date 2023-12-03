using MovieTicketReservation.Models;

namespace MovieTicketReservation.ViewModels
{
    public class EditShowsAndDisplayMoviesViewModel
    {
        public MovieShows MovieShows { get; set; } // This model will be edited
        public IEnumerable<Movies> Movies { get; set; } // This model is for display only
        public IEnumerable<Halls> Halls { get; set; }
    }
}

