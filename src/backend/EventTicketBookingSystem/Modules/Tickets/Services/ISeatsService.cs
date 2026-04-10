using EventTicketSystem.Modules.Tickets.Models;

namespace EventTicketSystem.Modules.Tickets.Services;

public interface ISeatsService
{
    Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId);
    Task ReserveAsync(Guid seatId);
    Task MarkAsPaidAsync(Guid seatId);
    Task ReleaseAsync(Guid seatId);
}
