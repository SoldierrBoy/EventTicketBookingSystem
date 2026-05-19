using EventTicketSystem.Infrastructure.Data; 
using EventTicketSystem.Modules.Orders.Models;
using EventTicketSystem.Modules.Orders.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace EventTicketSystem.Modules.Orders.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _repo;
    private readonly AppDbContext _db;

    public OrdersService(IOrdersRepository repo, AppDbContext db)
    {
        _repo = repo;
        _db = db;
    }

    public Task<IEnumerable<Order>> GetMyOrdersAsync(Guid userId) =>
        _repo.GetByUserIdAsync(userId);

    public async Task<Order> CreateAsync(Guid userId, Guid eventId, Guid seatId)
    {
        using var transaction = await _db.Database.BeginTransactionAsync();

        try
        {

            var connection = _db.Database.GetDbConnection();
            using var command = connection.CreateCommand();
            
            command.Transaction = transaction.GetDbTransaction(); 
            command.CommandText = "SELECT \"Status\" FROM \"Seats\" WHERE \"Id\" = @seatId FOR UPDATE";
            
            var param = command.CreateParameter();
            param.ParameterName = "@seatId";
            param.Value = seatId;
            command.Parameters.Add(param);

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            var statusObj = await command.ExecuteScalarAsync();

            if (statusObj is null)
                throw new Exception("Місце не знайдено.");

            var status = statusObj.ToString();
            if (status != "Available")
                throw new Exception("Місце вже заброньовано або куплено.");

            await _db.Database.ExecuteSqlInterpolatedAsync($"UPDATE \"Seats\" SET \"Status\" = 'Reserved' WHERE \"Id\" = {seatId}");

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

            await _db.SaveChangesAsync(); 
            
            await transaction.CommitAsync();

            return order;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task CancelAsync(Guid orderId, Guid userId)
    {
        using var transaction = await _db.Database.BeginTransactionAsync();

        try
        {
            var order = await _repo.GetByIdAsync(orderId);
            if (order is null || order.UserId != userId)
                throw new Exception("Замовлення не знайдено або доступ заборонено.");

            if (order.Status == "Cancelled")
                throw new Exception("Замовлення вже скасовано.");

            await _repo.UpdateStatusAsync(orderId, "Cancelled");

            await _db.Database.ExecuteSqlInterpolatedAsync($"UPDATE \"Seats\" SET \"Status\" = 'Available' WHERE \"Id\" = {order.SeatId}");

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}