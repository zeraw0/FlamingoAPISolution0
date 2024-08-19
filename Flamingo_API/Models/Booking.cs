using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flamingo_API.Models
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey(nameof(Flight))]
        public int FlightIdFK { get; set; }

        [ForeignKey(nameof(User))]
        public int UserIdFK { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        [Required]
        [StringLength(20)]
        public string PNR { get; set; }

        [Required]
        public bool IsCancelled { get; set; }

        public User User { get; set; }
        public Flight Flight { get; set; }
    }
}
