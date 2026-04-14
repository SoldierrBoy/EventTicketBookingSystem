using EventTicketSystem.Modules.Orders.Repositories;
using EventTicketSystem.Modules.Payments.Models;
using EventTicketSystem.Modules.Payments.Repositories;

namespace EventTicketSystem.Modules.Payments.Services;

public class PaymentsService : IPaymentsService
{
    private readonly IPaymentsRepository _repo;
    private readonly IOrdersRepository _ordersRepo;

    public PaymentsService(IPaymentsRepository repo, IOrdersRepository ordersRepo)
    {
        _repo = repo;
        _ordersRepo = ordersRepo;
    }

    public async Task<bool> ProcessAsync(Guid orderId)
    {
        var order = await _ordersRepo.GetByIdAsync(orderId);
        if (order is null || order.Status != "Pending")
            return false;

        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Status = "Completed",
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(payment);
        await _ordersRepo.UpdateStatusAsync(orderId, "Paid");

        // TODO: publish payment.confirmed to RabbitMQ
        return true;
    }
}