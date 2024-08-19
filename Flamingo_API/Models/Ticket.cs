using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flamingo_API.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [ForeignKey(nameof(Booking))]
        public int BookingIdFK { get; set; }

        [Required]
        [StringLength(50)]
        public string SeatNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string PassengerName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public Booking Booking { get; set; }
    }
}
