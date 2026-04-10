using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Modules.Orders.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _db;
    public OrdersRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId) =>
        await _db.Set<Order>().Where(o => o.UserId == userId).ToListAsync();

    public async Task<Order?> GetByIdAsync(Guid id) =>
        await _db.Set<Order>().FindAsync(id);

    public async Task AddAsync(Order order)
    {
        _db.Set<Order>().Add(order);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(Guid id, string status)
    {
        var order = await GetByIdAsync(id);
        if (order is not null)
        {
            order.Status = status;
            await _db.SaveChangesAsync();
        }
    }
}