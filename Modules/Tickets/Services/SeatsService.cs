using EventTicketSystem.Modules.Tickets.Models;
using EventTicketSystem.Modules.Tickets.Repositories;

namespace EventTicketSystem.Modules.Tickets.Services;

public class SeatsService : ISeatsService
{
    private readonly ISeatsRepository _repo;
    public SeatsService(ISeatsRepository repo) => _repo = repo;

    public Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId) =>
        _repo.GetByEventIdAsync(eventId);

    public Task ReserveAsync(Guid seatId) =>
        _repo.UpdateStatusAsync(seatId, "Reserved");

    public Task MarkAsPaidAsync(Guid seatId) =>
        _repo.UpdateStatusAsync(seatId, "Paid");

    public Task ReleaseAsync(Guid seatId) =>
        _repo.UpdateStatusAsync(seatId, "Available");
}