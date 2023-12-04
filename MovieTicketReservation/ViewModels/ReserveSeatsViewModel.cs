using MovieTicketReservation.Models;
using System.Security.Policy;

namespace MovieTicketReservation.ViewModels
{
    public class ReserveSeatsViewModel
    {
        public int SeatReservationId { get; set; }
        public int ReservationId { get; set; }
        public int Seat { get; set; }
        public int MovieShowId { get; set; }
        public int Price { get; set; }
        public DateTime MovieShowDate { get; set; }
        public DateTime MovieShowStart { get; set; }
        public int HallCapacity { get; set; }
        public int HallRows { get; set; }
        public string? Title { get; set; }
        

    }



}
