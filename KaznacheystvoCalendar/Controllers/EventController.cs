using System.Security.Claims;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Interfaces.ISevices;
using Microsoft.AspNetCore.Mvc;

namespace KaznacheystvoCalendar.Controllers;

[ApiController]
[Route("api/")]
public class EventController:ControllerBase
{
    private readonly IEventService _eventService;
    
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> CalendarEventAsync([FromQuery] int year, int month)
    {
        var userRole = User.FindFirst("Role")?.Value;
        var userDepartament = User.FindFirst("Departament")?.Value;
        return Ok(await _eventService.GetCalendarEventsAsync(userRole, userDepartament, month, year));
    }
    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> EventAsync([FromQuery] QueryObject query)
    {
        var userRole = User.FindFirst("Role")?.Value;
        var userDepartament = User.FindFirst("Departament")?.Value;
        return Ok(await _eventService.GetEventsAsync(query,userRole, userDepartament));
    }

    [Route("Event/{id:int}")]
    [HttpGet]
    public async Task<IActionResult> EventByIdAsync([FromRoute]int id)
    {
        var isExist = await _eventService.GetEventByIdAsync(id);
        if(isExist == null)
            return NotFound();
        return Ok(isExist);
    }
}