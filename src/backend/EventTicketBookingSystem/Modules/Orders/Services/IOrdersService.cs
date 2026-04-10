using EventTicketSystem.Modules.Orders.Models;

namespace EventTicketSystem.Modules.Orders.Services;

public interface IOrdersService
{
    Task<IEnumerable<Order>> GetMyOrdersAsync(Guid userId);
    Task<Order> CreateAsync(Guid userId, Guid eventId, Guid seatId);
    Task CancelAsync(Guid orderId, Guid userId);
}
