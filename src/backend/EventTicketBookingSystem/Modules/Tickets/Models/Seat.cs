namespace EventTicketSystem.Modules.Tickets.Models;

public class Seat
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string Row { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Status { get; set; } = "Available"; // Available | Reserved | Paid
}
