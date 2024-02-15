// EventRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Repositories
{
    public class EventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task AddEventAsync(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(int eventId, Event @event)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

            if (existingEvent != null)
            {
                @event.EventId = eventId;
                _context.Entry(existingEvent).CurrentValues.SetValues(@event);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEventAsync(int eventId)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

            if (existingEvent != null)
            {
                _context.Events.Remove(existingEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}
