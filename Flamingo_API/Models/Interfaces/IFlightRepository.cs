using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flamingo_API.Models
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> SearchFlightsAsync(string origin, string destination, DateTime departureDate);
        Task<Flight> GetByIdAsync(int id);
        Task AddAsync(Flight flight);
        Task UpdateAsync(Flight flight);
        Task DeleteAsync(int id);
    }
}
