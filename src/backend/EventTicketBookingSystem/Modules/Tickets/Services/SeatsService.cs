using EventTicketSystem.Modules.Tickets.Models;
using EventTicketSystem.Modules.Tickets.Repositories;

namespace EventTicketSystem.Modules.Tickets.Services;

public class SeatsService : ISeatsService
{
    private readonly ISeatsRepository _repo;
    public SeatsService(ISeatsRepository repo) => _repo = repo;

    // 1. Отримати всі місця події
    public async Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId) =>
        await _repo.GetByEventIdAsync(eventId);

    // 2. Резервування
    public async Task ReserveAsync(Guid seatId)
    {
        var seat = await _repo.GetByIdAsync(seatId) 
            ?? throw new KeyNotFoundException("Місце не знайдене");

        if (seat.Status != "Available")
        {
            throw new InvalidOperationException($"Неможливо зарезервувати: місце вже має статус {seat.Status}");
        }

        await _repo.UpdateStatusAsync(seatId, "Reserved");
    }

    // 3. Оплата (той самий метод, який ми додавали)
    public async Task MarkAsPaidAsync(Guid seatId)
    {
        var seat = await _repo.GetByIdAsync(seatId) 
            ?? throw new KeyNotFoundException("Місце не знайдене");

        if (seat.Status == "Paid")
        {
            throw new InvalidOperationException("Це місце вже оплачене.");
        }

        await _repo.UpdateStatusAsync(seatId, "Paid");
    }

    // 4. Звільнення місця
    public async Task ReleaseAsync(Guid seatId)
    {
        var seat = await _repo.GetByIdAsync(seatId) 
            ?? throw new KeyNotFoundException("Місце не знайдене");

        if (seat.Status == "Paid")
        {
            throw new InvalidOperationException("Не можна звільнити вже оплачене місце.");
        }

        await _repo.UpdateStatusAsync(seatId, "Available");
    }
}