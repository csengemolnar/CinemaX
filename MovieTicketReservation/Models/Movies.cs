using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper;
using CsvHelper.Configuration;

namespace MovieTicketReservation.Models
{
    public class Movies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        [CsvHelper.Configuration.Attributes.Ignore]
        public int MovieId { get; set; }
        public required string Title { get; set; }
        public required string Actors { get; set; }
        public required int MovieLength { get; set; }
        public required string Genre { get; set; }
        public required string Director { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required string Description { get; set; }
        public required string ThumbNail { get; set; }
    }
}
