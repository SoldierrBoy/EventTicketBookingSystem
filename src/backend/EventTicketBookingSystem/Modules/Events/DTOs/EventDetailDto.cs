namespace EventTicketSystem.Modules.Events.DTOs;

public class EventDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime CreatedAt { get; set; }
}