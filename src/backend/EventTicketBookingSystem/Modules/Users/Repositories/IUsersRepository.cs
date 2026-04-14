using EventTicketSystem.Modules.Users.Models;

namespace EventTicketSystem.Modules.Users.Repositories;

public interface IUsersRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
}
