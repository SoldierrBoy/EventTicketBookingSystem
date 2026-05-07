using EventTicketSystem.Modules.Events.DTOs;

namespace EventTicketSystem.Modules.Events.Services;

public interface IEventsService
{
    Task<IEnumerable<EventListItemDto>> GetEventsListAsync();
    Task<EventDetailDto?> GetEventDetailAsync(Guid id);
    Task CreateEventAsync(CreateEventDto dto);
    Task UpdateEventAsync(Guid id, CreateEventDto dto);
    Task DeleteEventAsync(Guid id);
}