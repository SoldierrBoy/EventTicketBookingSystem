using EventTicketSystem.Modules.Orders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventTicketSystem.Modules.Orders.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _service;
    public OrdersController(IOrdersService service) => _service = service;

    [HttpGet("my")]
    public async Task<IActionResult> GetMy()
    {
        var userId = GetUserId();
        if (userId == Guid.Empty)
            return Unauthorized(new { message = "Невалідний токен: неможливо визначити ID користувача." });
        var orders = await _service.GetMyOrdersAsync(userId);
        
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest req)
    {
        var userId = GetUserId();
        try
        {
            var order = await _service.CreateAsync(userId, req.EventId, req.SeatId);
            return CreatedAtAction(nameof(GetMy), new { id = order.Id }, order);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var userId = GetUserId();
        
        if (userId == Guid.Empty)
            return Unauthorized(new { message = "Невалідний токен: неможливо визначити ID користувача." });

        try
        {
            await _service.CancelAsync(id, userId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private Guid GetUserId()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(idClaim, out var id) ? id : Guid.Empty;
    }
}

public record CreateOrderRequest(Guid EventId, Guid SeatId);