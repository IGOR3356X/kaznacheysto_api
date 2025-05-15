using System.Security.Claims;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Interfaces.ISevices;
using Microsoft.AspNetCore.Mvc;

namespace KaznacheystvoCalendar.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController:ControllerBase
{
    private readonly IEventService _eventService;
    
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> CalendarEventAsync([FromQuery] int year, int month)
    {
        var userRole = User.FindFirst("Role")?.Value;
        var userDepartament = User.FindFirst("Departament")?.Value;
        return Ok(await _eventService.GetCalendarEventsAsync(userRole, userDepartament, month, year));
    }
    [HttpGet]
    public async Task<IActionResult> EventAsync([FromQuery] QueryObject query)
    {
        var userRole = User.FindFirst("Role")?.Value;
        var userDepartament = User.FindFirst("Departament")?.Value;
        return Ok(await _eventService.GetEventsAsync(query,userRole, userDepartament));
    }

    [HttpGet("{id}")]
    [ActionName("EventByIdAsync")]
    public async Task<IActionResult> EventByIdAsync([FromRoute]int id)
    {
        var isExist = await _eventService.GetEventByIdAsync(id);
        if(isExist == null)
            return NotFound();
        return Ok(isExist);
    }
    
    [HttpPost]
    public async Task<IActionResult> CrateEventAsync([FromBody] CreateEventDTO eventDto)
    {
        var createdEvent = await _eventService.CreateEventAsync(eventDto);
        return CreatedAtAction(nameof(EventByIdAsync), new { id = createdEvent.Id }, createdEvent);;
    }
    
    
}