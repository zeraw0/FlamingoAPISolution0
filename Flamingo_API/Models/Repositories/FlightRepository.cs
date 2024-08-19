using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flamingo_API.Models
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlamingoDbContext _context;

        public FlightRepository(FlamingoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(string origin, string destination, DateTime departureDate)
        {
            return await _context.Flights
                .Where(f => f.Origin == origin && f.Destination == destination && f.DepartureDate.Date == departureDate.Date)
                .ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task AddAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }
    }
}
