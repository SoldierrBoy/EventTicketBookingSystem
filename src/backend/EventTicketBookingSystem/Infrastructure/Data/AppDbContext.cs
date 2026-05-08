using Microsoft.EntityFrameworkCore;
using EventTicketSystem.Modules.Events.Models;
using EventTicketSystem.Modules.Users.Models;
using EventTicketSystem.Modules.Orders.Models;


namespace EventTicketSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

}