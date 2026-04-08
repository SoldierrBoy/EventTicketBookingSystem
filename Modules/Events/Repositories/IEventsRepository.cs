using EventTicketSystem.Modules.Events.Models;

namespace EventTicketSystem.Modules.Events.Repositories;

public interface IEventsRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(Guid id);
    Task AddAsync(Event ev);
    Task UpdateAsync(Event ev);
    Task DeleteAsync(Guid id);
}
