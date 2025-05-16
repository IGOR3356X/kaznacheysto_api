using KaznacheystvoCalendar.DTO.EventMember;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Interfaces.ISevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KaznacheystvoCalendar.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventMemberController:ControllerBase
{
    private readonly IEventMemberService _eventMemberService;
    public EventMemberController(IEventMemberService eventMemberService)
    {
        _eventMemberService = eventMemberService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> GetEventMembers([FromRoute]int id,[FromQuery] QueryObject queryObject)
    {
        var members = await _eventMemberService.GetEventMembers(queryObject,id);
        return Ok(members);
    }

    [HttpPost]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> CreateEventMembers(CreateEventMemberDTO dto)
    {
        var createdMember = await _eventMemberService.CreateEventMember(dto);
        if(createdMember == null)
            return Conflict(new { message = "Этот пользователь уже зарегестрирован на этот ивент" });
        return CreatedAtAction(nameof(GetEventMembers), new {id = createdMember.Id}, createdMember);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> DeleteEventMembers([FromRoute] int id)
    {
        var isExist = await _eventMemberService.DeleteEventMember(id);
        if(isExist == false)
            return NotFound();
        return NoContent();
    }
}