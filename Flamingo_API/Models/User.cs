using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flamingo_API.Models
{
    public class User
    {
        [Key]
        //[Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)] // Assuming a fixed length for the hash
        public string Password { get; set; }

        public string PhoneNo { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } // Admin or Customer

       // public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
