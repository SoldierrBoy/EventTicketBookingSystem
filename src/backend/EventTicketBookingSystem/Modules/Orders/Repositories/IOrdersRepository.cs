using EventTicketSystem.Modules.Orders.Models;

namespace EventTicketSystem.Modules.Orders.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
    Task<Order?> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
    Task UpdateStatusAsync(Guid id, string status);
}
