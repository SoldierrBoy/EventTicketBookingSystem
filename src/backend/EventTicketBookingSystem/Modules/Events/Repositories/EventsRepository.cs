using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Events.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Modules.Events.Repositories;

public class EventsRepository : IEventsRepository
{
    private readonly AppDbContext _db;
    public EventsRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Event>> GetAllAsync() =>
        await _db.Set<Event>().ToListAsync();

    public async Task<Event?> GetByIdAsync(Guid id) =>
        await _db.Set<Event>().FindAsync(id);

    public async Task AddAsync(Event ev)
    {
        _db.Set<Event>().Add(ev);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event ev)
    {
        _db.Set<Event>().Update(ev);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var ev = await GetByIdAsync(id);
        if (ev is not null)
        {
            _db.Set<Event>().Remove(ev);
            await _db.SaveChangesAsync();
        }
    }
}