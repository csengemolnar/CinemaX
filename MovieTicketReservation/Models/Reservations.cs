using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketReservation.Models
{

    public enum State
    {
        RESERVED,
        TOOKOVER
    }
    public class Reservations
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }
        
        public required DateTime Created { get; set; }
        [ForeignKey("MovieShows")]
        public required int MovieShowId { get; set; }
        [ForeignKey("AspNetUsers")]
        public required string UserId { get; set; }

        public string CurrentState { get; set; } = Enum.GetName(typeof(State), State.RESERVED)!;
    }
}
