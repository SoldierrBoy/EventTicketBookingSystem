using EventTicketSystem.Modules.Users.DTOs; 
using EventTicketSystem.Modules.Users.Models;
using EventTicketSystem.Modules.Users.Repositories;
using BCrypt.Net; 

namespace EventTicketSystem.Modules.Users.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repo;

    public UsersService(IUsersRepository repo) => _repo = repo;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = hashedPassword, 
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(user);

        return new AuthResponse("jwt-token-placeholder", user.Email, user.Role);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repo.GetByEmailAsync(request.Email)
            ?? throw new Exception("Invalid credentials");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new Exception("Invalid credentials");
        }

        return new AuthResponse("jwt-token-placeholder", user.Email, user.Role);
    }
}