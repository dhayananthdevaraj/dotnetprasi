
// EventService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class EventService
    {
        private readonly EventRepository _eventRepository;

        public EventService(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllEventsAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _eventRepository.GetEventByIdAsync(eventId);
        }

        public async Task AddEventAsync(Event @event)
        {
            await _eventRepository.AddEventAsync(@event);
        }

        public async Task UpdateEventAsync(int eventId, Event @event)
        {
            await _eventRepository.UpdateEventAsync(eventId, @event);
        }

        public async Task DeleteEventAsync(int eventId)
        {
            await _eventRepository.DeleteEventAsync(eventId);
        }
    }
}
