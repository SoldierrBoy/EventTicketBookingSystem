using System.Security.Claims; 
using EventTicketSystem.Modules.Users.DTOs;
using EventTicketSystem.Modules.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Modules.Users.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _service;
    public UsersController(IUsersService service) => _service = service;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _service.RegisterAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _service.LoginAsync(request);
        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized("Невірний токен");
        }

        var profile = await _service.GetProfileAsync(userId);

        return Ok(profile);
    }
    
    [HttpGet("all")]
    [Authorize(Roles = "Admin")] // Тільки для користувачів з Role = "Admin"
    public IActionResult GetAllUsers()
    {
        return Ok("Тут міг би бути список усіх користувачів, доступний лише адміну.");
    }
}
