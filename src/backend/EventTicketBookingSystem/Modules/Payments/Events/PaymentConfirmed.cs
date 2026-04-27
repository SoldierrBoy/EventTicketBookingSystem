namespace EventTicketSystem.Modules.Payments.Events;

// Це і є та сама подія payment.confirmed
public record PaymentConfirmed(Guid OrderId, DateTime ConfirmedAt);