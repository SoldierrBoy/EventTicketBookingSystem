using EventTicketSystem.Modules.Orders.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Modules.Orders.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _service;
    public OrdersController(IOrdersService service) => _service = service;

    [HttpGet("my")]
    public async Task<IActionResult> GetMy()
    {
        // TODO: get userId from JWT claims
        var userId = Guid.Empty;
        return Ok(await _service.GetMyOrdersAsync(userId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest req)
    {
        // TODO: get userId from JWT claims
        var userId = Guid.Empty;
        var order = await _service.CreateAsync(userId, req.EventId, req.SeatId);
        return CreatedAtAction(null, new { id = order.Id }, order);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var userId = Guid.Empty;
        await _service.CancelAsync(id, userId);
        return NoContent();
    }
}

public record CreateOrderRequest(Guid EventId, Guid SeatId);
