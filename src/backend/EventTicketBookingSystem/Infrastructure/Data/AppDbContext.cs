using Microsoft.EntityFrameworkCore;
using EventTicketSystem.Modules.Events.Models;
using EventTicketSystem.Modules.Locations.Models;

namespace EventTicketSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Seat> Seats { get; set; }
}