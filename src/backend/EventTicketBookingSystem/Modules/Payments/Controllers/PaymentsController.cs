using EventTicketSystem.Modules.Payments.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Modules.Payments.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentsService _service;

    public PaymentsController(IPaymentsService service)
    {
        _service = service;
    }

    [HttpPost("{orderId:guid}/pay")]
    public async Task<IActionResult> Pay(Guid orderId)
    {
        var result = await _service.ProcessAsync(orderId);
        if (!result)
        {
            return BadRequest(new { message = "Не вдалося обробити платіж. Перевірте статус замовлення." });
        }
        return Ok(new { message = "Оплата успішна" });
    }
}