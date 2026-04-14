namespace EventTicketSystem.Modules.Orders.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SeatId { get; set; }
    public Guid EventId { get; set; }
    public string Status { get; set; } = "Pending"; // Pending | Paid | Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
