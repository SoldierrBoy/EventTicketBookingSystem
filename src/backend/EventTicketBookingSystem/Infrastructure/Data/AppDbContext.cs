using EventTicketSystem.Modules.Events.Models;
using EventTicketSystem.Modules.Locations.Models;
using EventTicketSystem.Modules.Orders.Models;
using EventTicketSystem.Modules.Payments.Models;
using EventTicketSystem.Modules.Tickets.Models;
using EventTicketSystem.Modules.Users.Models;
using Microsoft.EntityFrameworkCore;


namespace EventTicketSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<EventTicketSystem.Modules.Locations.Models.Seat> Seats { get; set; }
}