namespace EventTicketSystem.Modules.Locations.Models
{
    public class Seat
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public string Row { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Status { get; set; } = "Available"; // Додано для модуля оплат!
    }
}
