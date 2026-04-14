using EventTicketSystem.Infrastructure.Data;
using EventTicketSystem.Modules.Payments.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Modules.Payments.Repositories;

public interface IPaymentsRepository
{
    Task AddAsync(Payment payment);
    Task<Payment?> GetByOrderIdAsync(Guid orderId);
}

public class PaymentsRepository : IPaymentsRepository
{
    private readonly AppDbContext _db;
    public PaymentsRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Payment payment)
    {
        _db.Set<Payment>().Add(payment);
        await _db.SaveChangesAsync();
    }

    public async Task<Payment?> GetByOrderIdAsync(Guid orderId) =>
        await _db.Set<Payment>().FirstOrDefaultAsync(p => p.OrderId == orderId);
}