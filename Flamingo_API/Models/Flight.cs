using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flamingo_API.Models
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [StringLength(100)]
        public string Origin { get; set; }

        [Required]
        [StringLength(100)]
        public string Destination { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TotalNumberOfSeats { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableSeats { get; set; }
    }
}
