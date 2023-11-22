using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketReservation.Models
{
    public class Seats
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }
        [ForeignKey("Hall")]
        public required int HallId { get; set; }
        public required int Row { get; set; }
        public required bool SeatStatus { get; set; }
        [ForeignKey("SeatReservations")]
        public required int SeatReservationId { get; set; }
        public required int SeatNumber { get; set; }

    }
}
