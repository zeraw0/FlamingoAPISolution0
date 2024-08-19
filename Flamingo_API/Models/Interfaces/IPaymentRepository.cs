using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flamingo_API.Models
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(int id);
        Task AddAsync(Payment payment);
    }
}
