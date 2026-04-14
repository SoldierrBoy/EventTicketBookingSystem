using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Modules.Users.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _db;
    public UsersRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByEmailAsync(string email) =>
        await _db.Set<User>().FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _db.Set<User>().FindAsync(id);

    public async Task AddAsync(User user)
    {
        _db.Set<User>().Add(user);
        await _db.SaveChangesAsync();
    }
}
