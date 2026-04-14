using EventTicketSystem.Modules.Users.DTOs;

namespace EventTicketSystem.Modules.Users.Services;

public interface IUsersService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<UserProfileResponse> GetProfileAsync(Guid userId);
}
