namespace MovieTicketReservation.ViewModels
{
    public class ChangeReservationStatusViewModel
    {
        public int SeatReservationId { get; set; }
        public int ReservationId { get; set; }
        public string Status { get; set; }
    }
}
