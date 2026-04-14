using EventTicketSystem.Modules.Tickets.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Modules.Tickets.Controllers;

[ApiController]
[Route("api/events/{eventId:guid}/seats")]
public class SeatsController : ControllerBase
{
    private readonly ISeatsService _service;
    public SeatsController(ISeatsService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetByEvent(Guid eventId) =>
        Ok(await _service.GetByEventIdAsync(eventId));
}
