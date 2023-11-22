using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieTicketReservation.Models
{
    public class Halls
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [CsvHelper.Configuration.Attributes.Ignore]
        public int HallId { get; set; }
        public required int Capacity { get; set; }
        public required int Rows { get; set; }

    }
}
