using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketReservation.Models
{
    public class MovieShows
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [CsvHelper.Configuration.Attributes.Ignore]
        public int MovieShowId { get; set; }
        public required DateTime Date { get; set; }
        [ForeignKey("Hall")]
        public required int HallId { get; set; }
        [ForeignKey("Movies")]
        public required int MovieId { get; set; }
        public required DateTime Start { get; set; } 
    }
}
