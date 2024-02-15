
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var events = await _context.Events.ToListAsync();

            return Ok(events);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<Event>> GetEventById(int eventId)
        {
            var @event = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (@event == null)
            {
                return NotFound(new { message = "Cannot find any event" });
            }

            return Ok(@event);
        }

        [HttpPost]
        public async Task<ActionResult> AddEvent([FromBody] Event @event)
        {
            try
            {
                _context.Events.Add(@event);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Event added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{eventId}")]
        public async Task<ActionResult> UpdateEvent(int eventId, [FromBody] Event @event)
        {
            try
            {
                var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

                if (existingEvent == null)
                {
                    return NotFound(new { message = "Cannot find any event" });
                }

                @event.EventId = eventId;

                _context.Entry(existingEvent).CurrentValues.SetValues(@event);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Event updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {
            try
            {
                var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

                if (@event == null)
                {
                    return NotFound(new { message = "Cannot find any event" });
                }

                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Event deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
