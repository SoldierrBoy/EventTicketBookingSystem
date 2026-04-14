namespace EventTicketSystem.Modules.Payments.Models;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; } = "Pending"; // Pending | Completed | Failed
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
