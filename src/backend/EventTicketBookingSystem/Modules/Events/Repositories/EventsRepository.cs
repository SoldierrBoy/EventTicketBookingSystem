using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Events.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Modules.Events.Repositories;

public class EventsRepository : IEventsRepository
{
    private readonly AppDbContext _context;

    public EventsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task AddAsync(Event ev)
    {
        await _context.Events.AddAsync(ev);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event ev)
    {
        _context.Events.Update(ev);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var ev = await GetByIdAsync(id);
        if (ev != null)
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }
}