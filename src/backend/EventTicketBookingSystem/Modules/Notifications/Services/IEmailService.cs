namespace EventTicketSystem.Modules.Notifications.Services;

public interface IEmailService
{
    Task SendBookingConfirmationAsync(string toEmail, Guid orderId);
}
