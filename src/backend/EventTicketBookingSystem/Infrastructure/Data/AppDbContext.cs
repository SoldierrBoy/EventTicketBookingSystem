using Microsoft.EntityFrameworkCore;
using EventTicketSystem.Modules.Events.Models;

namespace EventTicketSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
}