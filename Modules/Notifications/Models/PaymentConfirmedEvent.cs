namespace EventTicketSystem.Modules.Notifications.Models;

public record PaymentConfirmedEvent(Guid OrderId, Guid UserId, string UserEmail);
