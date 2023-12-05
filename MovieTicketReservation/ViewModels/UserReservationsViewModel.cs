using MovieTicketReservation.Models;
   
namespace MovieTicketReservation.ViewModels
{
    public class UserReservationsViewModel
    {
        public DateTime ReservationDate { get; set; }
        public string MovieTitle { get; set; }
        
        public DateTime ShowDate { get; set; }
        public int Hall { get; set; }
        public int ReservedSeat { get; set; }
        public int Price { get; set; }
        public string CurrentState { get; set; }

    }
}
