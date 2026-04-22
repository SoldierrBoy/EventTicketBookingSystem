using EventTicketSystem.Modules.Orders.Repositories;
using EventTicketSystem.Modules.Payments.Models;
using EventTicketSystem.Modules.Payments.Repositories;
using EventTicketSystem.Modules.Payments; // Додай цей namespace для PaymentConfirmed
using MassTransit;

namespace EventTicketSystem.Modules.Payments.Services;

public class PaymentsService : IPaymentsService
{
    private readonly IPaymentsRepository _repo;
    private readonly IOrdersRepository _ordersRepo;
    private readonly IPublishEndpoint _publishEndpoint; // 1. Додаємо поле

    public PaymentsService(
        IPaymentsRepository repo, 
        IOrdersRepository ordersRepo, 
        IPublishEndpoint publishEndpoint) // 2. Додаємо в ін'єкцію
    {
        _repo = repo;
        _ordersRepo = ordersRepo;
        _publishEndpoint = publishEndpoint;
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

        // Зберігаємо платіж
        await _repo.AddAsync(payment);
        
        // Оновлюємо статус замовлення
        await _ordersRepo.UpdateStatusAsync(orderId, "Paid");

        // ПУШ ПОДІЇ В RABBITMQ
        // Тепер _publishEndpoint доступний
        await _publishEndpoint.Publish(new PaymentConfirmed(orderId, payment.CreatedAt));

        return true;
    }
}