using EventTicketSystem.Modules.Payments.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EventTicketSystem.Modules.Notifications.Consumers;

public class PaymentConfirmedConsumer : IConsumer<PaymentConfirmed>
{
    private readonly ILogger<PaymentConfirmedConsumer> _logger;

    public PaymentConfirmedConsumer(ILogger<PaymentConfirmedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<PaymentConfirmed> context)
    {
        var message = context.Message;

        _logger.LogInformation($"[RabbitMQ] Отримано підтвердження оплати! Замовлення: {message.OrderId}");

        // TODO: Пізніше тут можна викликати IEmailService для відправки листа з квитком

        return Task.CompletedTask;
    }
}