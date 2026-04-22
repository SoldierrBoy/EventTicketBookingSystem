namespace EventTicketSystem.Modules.Tickets.DTOs;

public record SeatResponse(Guid Id, Guid EventId, string Row, int Number, string Status);