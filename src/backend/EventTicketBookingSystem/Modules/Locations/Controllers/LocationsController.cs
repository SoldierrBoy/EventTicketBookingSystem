using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventTicketSystem.Modules.Locations.DTOs;
using EventTicketSystem.Modules.Locations.Services;

namespace EventTicketSystem.Modules.Locations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        // GET /api/locations — список залів
        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            var locations = await _locationsService.GetAllLocationsAsync();
            return Ok(locations);
        }

        // POST /api/locations — створити зал (Admin only)
        [HttpPost]
      [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto dto)
        {
            var location = await _locationsService.CreateLocationAsync(dto);
            // Повертаємо 201 Created та сам об'єкт
            return StatusCode(201, location); 
        }

        // POST /api/locations/{locationId}/seats — додати місця в зал (Admin only)
        [HttpPost("{locationId:guid}/seats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSeats(Guid locationId, [FromBody] List<CreateSeatDto> seats)
        {
            await _locationsService.AddSeatsAsync(locationId, seats);
            return Ok(new { Message = $"Місця успішно додано до залу {locationId}" });
        }
    }
}