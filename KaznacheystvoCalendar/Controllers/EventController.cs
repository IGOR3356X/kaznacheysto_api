using System.Security.Claims;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Interfaces.ISevices;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Сотрудник,Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> CalendarEventAsync([FromQuery] int year, int month)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var userDepartament = User.FindFirst("Departament")?.Value;
        return Ok(await _eventService.GetCalendarEventsAsync(userRole, userDepartament, month, year));
    }
    [HttpGet]
    [Authorize(Roles = "Сотрудник,Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> EventAsync([FromQuery] QueryObject query)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var userDepartament = User.FindFirst("Departament")?.Value;
        return Ok(await _eventService.GetEventsAsync(query,userRole, userDepartament));
    }
    
    [HttpGet("UserEvents/{userId:int}")]
    [Authorize(Roles = "Сотрудник,Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> GetUserEvent([FromQuery] QueryObject query, [FromRoute] int userId)
    {
        return Ok(await _eventService.GetUserEventAsync(query,userId));
    }

    [HttpGet("{id}")]
    [ActionName("EventByIdAsync")]
    [Authorize(Roles = "Сотрудник,Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> EventByIdAsync([FromRoute]int id)
    {
        var isExist = await _eventService.GetEventByIdAsync(id);
        if(isExist == null)
            return NotFound();
        return Ok(isExist);
    }
    
    [HttpPost]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> CrateEventAsync([FromBody] CreateEventDTO eventDto)
    {
        var createdEvent = await _eventService.CreateEventAsync(eventDto);
        return CreatedAtAction(nameof(EventByIdAsync), new { id = createdEvent.Id }, createdEvent);;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> UpdateEventAsync([FromRoute] int id, [FromBody] UpdateEventDTO eventDto)
    {
        var isExist = await _eventService.UpdateEventAsync(id, eventDto);
        if(isExist == false)
            return NotFound();
        return Ok();
    }
}