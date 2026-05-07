namespace EventTicketSystem.Modules.Events.DTOs;

public class EventListItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
}