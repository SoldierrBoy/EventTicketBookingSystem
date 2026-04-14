using EventTicketSystem.Modules.Events.Models;
using EventTicketSystem.Modules.Events.Repositories;

namespace EventTicketSystem.Modules.Events.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _repo;
    public EventsService(IEventsRepository repo) => _repo = repo;

    public Task<IEnumerable<Event>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Event?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public async Task<Event> CreateAsync(Event ev)
    {
        ev.Id = Guid.NewGuid();
        ev.CreatedAt = DateTime.UtcNow;
        await _repo.AddAsync(ev);
        return ev;
    }

    public Task UpdateAsync(Event ev) => _repo.UpdateAsync(ev);
    public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
}