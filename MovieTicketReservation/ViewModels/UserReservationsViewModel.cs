using MovieTicketReservation.Models;
   
namespace MovieTicketReservation.ViewModels
{
    public class UserReservationsViewModel
    {
        public int ReservationId { get; set; }
        public int SeatReservationId {get;set;}
        public DateTime ReservationDate { get; set; }
        public string MovieTitle { get; set; }
        
        public DateTime ShowDate { get; set; }
        public int Hall { get; set; }
        public int ReservedSeat { get; set; }
        public int Price { get; set; }
        public string CurrentState { get; set; }

    }
}
