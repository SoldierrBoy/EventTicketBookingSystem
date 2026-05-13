using EventTicketSystem.Modules.Locations.DTOs;
using EventTicketSystem.Modules.Locations.Models;

namespace EventTicketSystem.Modules.Locations.Services
{
    public interface ILocationsService
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location> CreateLocationAsync(CreateLocationDto dto);
        Task AddSeatsAsync(Guid locationId, List<CreateSeatDto> seats);
    }
}