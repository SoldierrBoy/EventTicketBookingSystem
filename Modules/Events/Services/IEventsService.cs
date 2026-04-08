using EventTicketSystem.Modules.Events.Models;

namespace EventTicketSystem.Modules.Events.Services;

public interface IEventsService
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(Guid id);
    Task<Event> CreateAsync(Event ev);
    Task UpdateAsync(Event ev);
    Task DeleteAsync(Guid id);
}
