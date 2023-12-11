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

        [StringLength(100, MinimumLength = 3)]
        public required string Title { get; set; }
        [StringLength(200, MinimumLength = 3)]
        public required string Actors { get; set; }
        [Range(60, 300, ErrorMessage = "The length of the movie must be between 60 and 300 minutes.")]
        public required int MovieLength { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public required string Genre { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public required string Director { get; set; }
        [DataType(DataType.Date)]
        public required DateTime ReleaseDate { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public required string Description { get; set; }
        
        public required string ThumbNail { get; set; }
        

    }
}
