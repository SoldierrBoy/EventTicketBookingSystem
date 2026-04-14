using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventTicketSystem.Modules.Users.DTOs; // Виправлено: прибрали .Models
using EventTicketSystem.Modules.Users.Models;
using EventTicketSystem.Modules.Users.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace EventTicketSystem.Modules.Users.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repo;
    private readonly IConfiguration _config;

    public UsersService(IUsersRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // 1. ПЕРЕВІРКА НА ДУБЛІКАТ (Те, що ми додавали раніше)
        var existingUser = await _repo.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

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

        var token = GenerateJwtToken(user);
        return new AuthResponse(token, user.Email, user.Role);
    }
    public async Task<UserProfileResponse> GetProfileAsync(Guid userId)
    {
        var user = await _repo.GetByIdAsync(userId)
            // Використовуємо KeyNotFoundException для статусу 404
            ?? throw new KeyNotFoundException("Користувача не знайдено");

        return new UserProfileResponse(user.Id, user.Email, user.Role, user.CreatedAt);
    }
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repo.GetByEmailAsync(request.Email);

        // 2. ВАРІАНТ ДЛЯ REST API: 401 Unauthorized замість звичайного Exception
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = GenerateJwtToken(user);
        return new AuthResponse(token, user.Email, user.Role);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _config.GetSection("Jwt");
        var secretKey = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is missing");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            // Можна додати дані з конфігу для Issuer/Audience
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}
