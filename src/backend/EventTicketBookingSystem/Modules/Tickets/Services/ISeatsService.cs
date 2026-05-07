using EventTicketSystem.Modules.Tickets.Models;
using EventTicketSystem.Modules.Tickets.DTOs;
namespace EventTicketSystem.Modules.Tickets.Services;

public interface ISeatsService
{
    Task<IEnumerable<SeatResponse>> GetByEventIdAsync(Guid eventId);
    Task ReserveAsync(Guid seatId);
    Task MarkAsPaidAsync(Guid seatId);
    Task ReleaseAsync(Guid seatId);
}
