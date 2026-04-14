namespace EventTicketSystem.Modules.Notifications.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    public EmailService(ILogger<EmailService> logger) => _logger = logger;

    public async Task SendBookingConfirmationAsync(string toEmail, Guid orderId)
    {
        // TODO: integrate real SMTP / SendGrid
        _logger.LogInformation("Sending booking confirmation to {Email} for order {OrderId}", toEmail, orderId);
        await Task.CompletedTask;
    }
}
