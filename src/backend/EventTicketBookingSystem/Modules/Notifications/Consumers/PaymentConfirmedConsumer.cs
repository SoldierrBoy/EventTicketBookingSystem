using EventTicketSystem.Modules.Notifications.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventTicketSystem.Modules.Notifications.Consumers;

public class PaymentConfirmedConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<PaymentConfirmedConsumer> _logger;

    public PaymentConfirmedConsumer(
        IServiceScopeFactory scopeFactory,
        ILogger<PaymentConfirmedConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: підключити RabbitMQ і консьюмити payment.confirmed
        _logger.LogInformation("PaymentConfirmedConsumer started");
        await Task.CompletedTask;
    }
}