using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/venue")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetAllVenues()
        {
            var venues = await _context.Venues.ToListAsync();

            return Ok(venues);
        }

        [HttpGet("{venueId}")]
        public async Task<ActionResult<Venue>> GetVenueById(int venueId)
        {
            var venue = await _context.Venues
                .FirstOrDefaultAsync(v => v.VenueId == venueId);

            if (venue == null)
            {
                return NotFound(new { message = "Cannot find any venue" });
            }

            return Ok(venue);
        }

        [HttpPost]
        public async Task<ActionResult> AddVenue([FromBody] Venue venue)
        {
            try
            {
                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Venue added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{venueId}")]
        public async Task<ActionResult> UpdateVenue(int venueId, [FromBody] Venue venue)
        {
            try
            {
                var existingVenue = await _context.Venues.FirstOrDefaultAsync(v => v.VenueId == venueId);

                if (existingVenue == null)
                {
                    return NotFound(new { message = "Cannot find any venue" });
                }

                venue.VenueId = venueId;

                _context.Entry(existingVenue).CurrentValues.SetValues(venue);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Venue updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{venueId}")]
        public async Task<ActionResult> DeleteVenue(int venueId)
        {
            try
            {
                var venue = await _context.Venues.FirstOrDefaultAsync(v => v.VenueId == venueId);

                if (venue == null)
                {
                    return NotFound(new { message = "Cannot find any venue" });
                }

                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Venue deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}



// To work with Entity Framework Core:

// Install EF using the following commands

// :

//  dotnet new tool-manifest

 

// dotnet tool install --local dotnet-ef --version 6.0.6

 

// dotnet dotnet-ef --To check the EF installed or not



// dotnet dotnet-ef migrations add "InitialSetup" --command to setup the initial creation of tables mentioned in DBContext

 

// dotnet dotnet-ef database update --command to update the database
