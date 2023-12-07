namespace MovieTicketReservation.ViewModels
{
    public class AllReservationsViewModel
    {
        public int ReservationId { get; set; }
        public int SeatReservationId { get; set; }
        public string ReserverEmail { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ShowDate { get; set; }
        public int Hall { get; set; }
        public List<SeatReservationInfo> Seats { get; set; }
        public int Price { get; set; }
        public string CurrentState { get; set; }
    }
    public class SeatReservationInfo
    {
        public int SeatReservationId { get; set; }
        public int ReservedSeat { get; set; }
        
    }
}
