using EventTicketSystem.Modules.Users.DTOs;
using EventTicketSystem.Modules.Users.Services;
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
}
