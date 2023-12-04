using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketReservation.Models
{
    public class SeatReservations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatReservationId { get; set; }
        [ForeignKey("Reservation")]
        public required int ReservationId { get; set; }
        
        public required int Seat { get; set; }

        [ForeignKey("MovieShows")]
        public required int MovieShowId { get; set; }

        public required int Price { get; set; }

    }
}
