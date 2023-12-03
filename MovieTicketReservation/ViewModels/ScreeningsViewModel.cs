using MovieTicketReservation.Models;

namespace MovieTicketReservation.ViewModels
{
    public class ScreeningsViewModel
    {
        public Movies Movie { get; set; }
        public List<MovieShows> MovieShows { get; set; }
    }
}
