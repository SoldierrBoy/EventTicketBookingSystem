using EventTicketSystem.Modules.Orders.Models;
using EventTicketSystem.Modules.Orders.Repositories;

namespace EventTicketSystem.Modules.Orders.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _repo;
    public OrdersService(IOrdersRepository repo) => _repo = repo;

    public Task<IEnumerable<Order>> GetMyOrdersAsync(Guid userId) =>
        _repo.GetByUserIdAsync(userId);

    public async Task<Order> CreateAsync(Guid userId, Guid eventId, Guid seatId)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            EventId = eventId,
            SeatId = seatId,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };
        await _repo.AddAsync(order);
        return order;
    }

    public async Task CancelAsync(Guid orderId, Guid userId)
    {
        var order = await _repo.GetByIdAsync(orderId);
        if (order is null || order.UserId != userId)
            throw new Exception("Order not found or access denied");
        await _repo.UpdateStatusAsync(orderId, "Cancelled");
    }
}