namespace EventTicketSystem.Modules.Locations.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}