using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSets will be added here as each module is implemented
    // e.g. public DbSet<User> Users => Set<User>();
}
