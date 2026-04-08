using EventTicketSystem.Modules.Payments.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Modules.Payments.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentsService _service;
    public PaymentsController(IPaymentsService service) => _service = service;

    [HttpPost("{orderId:guid}/pay")]
    public async Task<IActionResult> Pay(Guid orderId)
    {
        var success = await _service.ProcessAsync(orderId);
        return success ? Ok(new { status = "Paid" }) : BadRequest(new { status = "Failed" });
    }
}
