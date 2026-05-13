using EventTicketSystem.Modules.Notifications.Services;
using EventTicketSystem.Modules.Payments.Events;
using EventTicketSystem.Modules.Orders.Repositories; 
using EventTicketSystem.Modules.Users.Repositories; 
using MassTransit;

namespace EventTicketSystem.Modules.Notifications.Consumers;

public class PaymentConfirmedConsumer : IConsumer<PaymentConfirmed>
{
    private readonly IEmailService _emailService;
    private readonly IOrdersRepository _ordersRepo;
    private readonly IUsersRepository _usersRepo;

    public PaymentConfirmedConsumer(
        IEmailService emailService,
        IOrdersRepository ordersRepo,
        IUsersRepository usersRepo)
    {
        _emailService = emailService;
        _ordersRepo = ordersRepo;
        _usersRepo = usersRepo;
    }

    public async Task Consume(ConsumeContext<PaymentConfirmed> context)
    {
        var orderId = context.Message.OrderId;

        var order = await _ordersRepo.GetByIdAsync(orderId);
        if (order == null) return; 

        var user = await _usersRepo.GetByIdAsync(order.UserId);
        if (user == null) return; 

        await _emailService.SendBookingConfirmationAsync(user.Email, orderId);
    }
}