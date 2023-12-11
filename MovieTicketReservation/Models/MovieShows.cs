using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MovieTicketReservation.Validation;

namespace MovieTicketReservation.Models
{
    public class MovieShows
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [CsvHelper.Configuration.Attributes.Ignore]
        public int MovieShowId { get; set; }

        [DataType(DataType.Date)]
        
        public required DateTime Date { get; set; }
        [ForeignKey("Hall")]
        [Range(1, 10)]
        public required int HallId { get; set; }

        [ForeignKey("Movies")]
        public required int MovieId { get; set; }

        [MovieShowDateAndStartMatch]
        [IsHallEmptyAtTheWantedDate]
        public required DateTime Start { get; set; }

    }
}
