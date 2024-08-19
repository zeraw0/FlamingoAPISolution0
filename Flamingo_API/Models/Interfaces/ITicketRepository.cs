using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flamingo_API.Models
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetByBookingIdAsync(int bookingId);
        Task AddAsync(Ticket ticket);
        Task DeleteAsync(int ticketId); // New method for deleting a ticket
    }
}
