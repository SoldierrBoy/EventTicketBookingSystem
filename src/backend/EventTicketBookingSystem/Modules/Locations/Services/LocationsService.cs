using EventTicketSystem.Modules.Locations.DTOs;
using EventTicketSystem.Modules.Locations.Models;
using EventTicketSystem.Modules.Locations.Repositories;

namespace EventTicketSystem.Modules.Locations.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsRepository _locationsRepository;

        public LocationsService(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _locationsRepository.GetAllAsync();
        }

        public async Task<Location> CreateLocationAsync(CreateLocationDto dto)
        {
            var location = new Location
            {
                Id = Guid.NewGuid(), // Генеруємо новий ID
                Name = dto.Name,
                Address = dto.Address
            };

            await _locationsRepository.AddAsync(location);
            return location;
        }

        public async Task AddSeatsAsync(Guid locationId, List<CreateSeatDto> seatsDto)
        {
            // Перетворюємо список DTO на список моделей для бази
            var seats = seatsDto.Select(dto => new Seat
            {
                Id = Guid.NewGuid(),
                LocationId = locationId,
                Row = dto.Row,
                Number = dto.Number
            }).ToList();

            await _locationsRepository.AddSeatsAsync(seats);
        }
    }
}