using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flamingo_API.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey(nameof(Booking))]
        public int BookingIdFK { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentType { get; set; } // CreditCard or DebitCard

        [Required]
        [StringLength(16, MinimumLength = 13)]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 5)]
        public string CardHolderName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public Booking Booking { get; set; }
    }
}
