using EventTicketSystem.Modules.Tickets.Models;

namespace EventTicketSystem.Modules.Tickets.Repositories;

public interface ISeatsRepository
{
    Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId);
    Task<Seat?> GetByIdAsync(Guid id);
    Task UpdateStatusAsync(Guid id, string status);
}
