namespace EventTicketSystem.Modules.Events.DTOs;

public class CreateEventDto
{
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
}