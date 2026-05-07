using EventTicketSystem.Modules.Events.DTOs;
using EventTicketSystem.Modules.Events.Models;
using EventTicketSystem.Modules.Events.Repositories;

namespace EventTicketSystem.Modules.Events.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _repository;

    public EventsService(IEventsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EventListItemDto>> GetEventsListAsync()
    {
        var events = await _repository.GetAllAsync();
        return events.Select(e => new EventListItemDto
        {
            Id = e.Id,
            Title = e.Title,
            Type = e.Type,
            StartsAt = e.StartsAt
        });
    }

    public async Task<EventDetailDto?> GetEventDetailAsync(Guid id)
    {
        var ev = await _repository.GetByIdAsync(id);
        if (ev == null) return null;

        return new EventDetailDto
        {
            Id = ev.Id,
            Title = ev.Title,
            Type = ev.Type,
            Venue = ev.Venue,
            StartsAt = ev.StartsAt,
            CreatedAt = ev.CreatedAt
        };
    }

    public async Task CreateEventAsync(CreateEventDto dto)
    {
        var ev = new Event
        {
            Title = dto.Title,
            Type = dto.Type,
            Venue = dto.Venue,
            StartsAt = dto.StartsAt
        };
        await _repository.AddAsync(ev);
    }

    public async Task UpdateEventAsync(Guid id, CreateEventDto dto)
    {
        var ev = await _repository.GetByIdAsync(id);
        if (ev != null)
        {
            ev.Title = dto.Title;
            ev.Type = dto.Type;
            ev.Venue = dto.Venue;
            ev.StartsAt = dto.StartsAt;
            await _repository.UpdateAsync(ev);
        }
    }

    public async Task DeleteEventAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}