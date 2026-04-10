using EventTicketSystem.Modules.Users.DTOs;
using EventTicketSystem.Modules.Users.Models;
using EventTicketSystem.Modules.Users.Repositories;

namespace EventTicketSystem.Modules.Users.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repo;

    public UsersService(IUsersRepository repo) => _repo = repo;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // TODO: hash password, generate JWT
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = request.Password, // replace with BCrypt
            Role = "User"
        };
        await _repo.AddAsync(user);
        return new AuthResponse("jwt-token-placeholder", user.Email, user.Role);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        // TODO: verify hash, generate JWT
        var user = await _repo.GetByEmailAsync(request.Email)
            ?? throw new Exception("Invalid credentials");
        return new AuthResponse("jwt-token-placeholder", user.Email, user.Role);
    }
}
