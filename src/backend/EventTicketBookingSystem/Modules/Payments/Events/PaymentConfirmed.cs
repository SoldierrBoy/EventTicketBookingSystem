namespace EventTicketSystem.Modules.Payments.Events;

// record автоматично створить властивості OrderId та CreatedAt
public record PaymentConfirmed(Guid OrderId, DateTime CreatedAt);