using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventTicketSystem.Modules.Users.Models.DTOs; 
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

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repo.GetByEmailAsync(request.Email)
            ?? throw new Exception("Invalid credentials");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new Exception("Invalid credentials");
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