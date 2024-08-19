using Microsoft.AspNetCore.Mvc;
using Flamingo_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Flamingo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _repo;

        public FlightController(IFlightRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights([FromQuery] string origin, [FromQuery] string destination, [FromQuery] DateTime departureDate)
        {
            var flights = await _repo.SearchFlightsAsync(origin, destination, departureDate);
            return Ok(flights);
        }

        // GET api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _repo.GetByIdAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        // POST api/Flight
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight([FromBody] Flight flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }

            await _repo.AddAsync(flight);
            return CreatedAtAction(nameof(GetFlight), new { id = flight.FlightId }, flight);
        }

        // PUT api/Flight/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, [FromBody] Flight flight)
        {
            // Check if the flight object is null
            if (flight == null)
            {
                return BadRequest("Flight data cannot be null.");
            }

            // Check if the ID in the URL matches the ID in the flight object
            if (id <= 0)
            {
                return BadRequest("Invalid flight ID.");
            }

            //if (id != flight.FlightId)
            //{
            //    return BadRequest("Flight ID mismatch.");
            //}

            // Fetch the existing flight from the database
            var existingFlight = await _repo.GetByIdAsync(id);
            if (existingFlight == null)
            {
                return NotFound($"Flight with ID {id} not found.");
            }

            // Update the existing flight with new data
            existingFlight.Origin = flight.Origin;
            existingFlight.Destination = flight.Destination;
            existingFlight.DepartureDate = flight.DepartureDate;
            existingFlight.ArrivalDate = flight.ArrivalDate;
            existingFlight.Price = flight.Price;
            existingFlight.TotalNumberOfSeats = flight.TotalNumberOfSeats;
            existingFlight.AvailableSeats = flight.AvailableSeats;

            // Save changes to the database
            try
            {
                await _repo.UpdateAsync(existingFlight);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details and return a server error
                Console.WriteLine($"Error updating flight: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the flight.");
            }

            // Return a no-content response to indicate successful update
            return NoContent();
        }




        // DELETE api/Flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _repo.GetByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
