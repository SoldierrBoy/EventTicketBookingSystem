namespace EventTicketSystem.Modules.Events.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Cinema | Concert
    public string Venue { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
