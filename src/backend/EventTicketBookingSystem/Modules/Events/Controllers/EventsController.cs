using EventTicketSystem.Modules.Events.DTOs;
using EventTicketSystem.Modules.Events.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Modules.Events.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventsService _eventsService;

    public EventsController(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    // feature/events-list: Повертає список подій через EventListItemDto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventListItemDto>>> GetAll()
    {
        var events = await _eventsService.GetEventsListAsync();
        return Ok(events);
    }

    // feature/events-detail: Повертає повну інформацію про подію або 404
    [HttpGet("{id}")]
    public async Task<ActionResult<EventDetailDto>> GetById(Guid id)
    {
        var ev = await _eventsService.GetEventDetailAsync(id);
        if (ev == null) return NotFound();
        return Ok(ev);
    }

    // feature/events-admin: Створення нової події
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateEventDto dto)
    {
        await _eventsService.CreateEventAsync(dto);
        return Ok();
    }

    // feature/events-admin: Оновлення існуючої події
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, CreateEventDto dto)
    {
        await _eventsService.UpdateEventAsync(id, dto);
        return NoContent();
    }

    // feature/events-admin: Видалення події
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _eventsService.DeleteEventAsync(id);
        return NoContent();
    }
}