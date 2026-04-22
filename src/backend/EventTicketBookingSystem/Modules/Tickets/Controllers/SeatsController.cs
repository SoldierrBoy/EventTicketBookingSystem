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

        // 2. Резервування місця
    [HttpPost("~/api/seats/{seatId:guid}/reserve")]
    public async Task<IActionResult> Reserve(Guid seatId)
    {
        try
        {
            await _service.ReserveAsync(seatId);
            return Ok(new { message = "Місце успішно зарезервовано" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // 3. Підтвердження оплати
    [HttpPost("~/api/seats/{seatId:guid}/pay")]
    public async Task<IActionResult> MarkAsPaid(Guid seatId)
    {
        try
        {
            await _service.MarkAsPaidAsync(seatId);
            return Ok(new { message = "Місце позначено як оплачене" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // 4. Звільнення місця
    [HttpPost("~/api/seats/{seatId:guid}/release")]
    public async Task<IActionResult> Release(Guid seatId)
    {
        try
        {
            await _service.ReleaseAsync(seatId);
            return Ok(new { message = "Місце звільнено" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
