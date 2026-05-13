using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Locations.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Modules.Locations.Repositories;

public class LocationsRepository : ILocationsRepository
{
    private readonly AppDbContext _db;

    public LocationsRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        // Include(l => l.Seats) завантажить зал одразу з його місцями
        return await _db.Set<Location>().Include(l => l.Seats).ToListAsync();
    }

    public async Task AddAsync(Location location)
    {
        _db.Set<Location>().Add(location);
        await _db.SaveChangesAsync();
    }

    public async Task AddSeatsAsync(IEnumerable<Seat> seats)
    {
        _db.Set<Seat>().AddRange(seats);
        await _db.SaveChangesAsync();
    }
}