namespace MovieTicketReservation.Models
{
    public class MovieAndScreeningsViewModel
    {
        public int MovieShowId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ShowDate { get; set; }
        public int Hall { get; set; }
    }
}
