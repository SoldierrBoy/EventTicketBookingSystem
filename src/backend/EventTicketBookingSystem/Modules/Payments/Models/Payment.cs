namespace EventTicketSystem.Modules.Payments.Models;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; } = "Completed";
    public DateTime CreatedAt { get; set; }
}