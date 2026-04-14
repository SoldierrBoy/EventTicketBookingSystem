namespace EventTicketSystem.Modules.Payments.Services;

public interface IPaymentsService
{
    Task<bool> ProcessAsync(Guid orderId);
}
