using EventTicketSystem.Modules.Orders.Repositories;
using EventTicketSystem.Modules.Payments.Models;
using EventTicketSystem.Modules.Payments.Repositories;
using EventTicketSystem.Modules.Payments.Events; // 1. Виправлено: тепер веде до папки Events
using EventTicketSystem.Modules.Tickets.Services; // 2. Додано для доступу до статусів місць
using MassTransit;

namespace EventTicketSystem.Modules.Payments.Services;

public class PaymentsService : IPaymentsService
{
    private readonly IPaymentsRepository _repo;
    private readonly IOrdersRepository _ordersRepo;
    private readonly ISeatsService _seatsService; // 3. Нове поле
    private readonly IPublishEndpoint _publishEndpoint;

    public PaymentsService(
        IPaymentsRepository repo, 
        IOrdersRepository ordersRepo, 
        ISeatsService seatsService, // 4. Додаємо в ін'єкцію
        IPublishEndpoint publishEndpoint) 
    {
        _repo = repo;
        _ordersRepo = ordersRepo;
        _seatsService = seatsService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> ProcessAsync(Guid orderId)
    {
        // Перевірка замовлення
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

        // 1. Зберігаємо платіж у БД
        await _repo.AddAsync(payment);
        
        // 2. Оновлюємо статус замовлення на "Paid"
        await _ordersRepo.UpdateStatusAsync(orderId, "Paid");

        // 3. Оновлюємо статус місця на "Paid"
        // Використовуємо SeatId із замовлення
        await _seatsService.MarkAsPaidAsync(order.SeatId);

        // 4. ПУШ події в RabbitMQ
        await _publishEndpoint.Publish(new PaymentConfirmed(orderId, payment.CreatedAt));

        return true;
    }
}