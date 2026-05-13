using EventTicketSystem.Modules.Locations.Models;

namespace EventTicketSystem.Modules.Locations.Repositories
{
    public interface ILocationsRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task AddAsync(Location location);
        Task AddSeatsAsync(IEnumerable<Seat> seats);
    }
}