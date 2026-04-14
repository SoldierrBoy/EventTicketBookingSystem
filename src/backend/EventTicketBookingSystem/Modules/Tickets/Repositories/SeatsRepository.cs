using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Tickets.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Modules.Tickets.Repositories;

public class SeatsRepository : ISeatsRepository
{
    private readonly AppDbContext _db;
    public SeatsRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId) =>
        await _db.Set<Seat>().Where(s => s.EventId == eventId).ToListAsync();

    public async Task<Seat?> GetByIdAsync(Guid id) =>
        await _db.Set<Seat>().FindAsync(id);

    public async Task UpdateStatusAsync(Guid id, string status)
    {
        var seat = await GetByIdAsync(id);
        if (seat is not null)
        {
            seat.Status = status;
            await _db.SaveChangesAsync();
        }
    }
}