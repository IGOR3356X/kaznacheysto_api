using KaznacheystvoCalendar.DTO.Location;
using KaznacheystvoCalendar.Interfaces.ISevices;
using KaznacheystvoCalendar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KaznacheystvoCalendar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController:ControllerBase
{
    private readonly ILocationService _locationService;
    
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> GetLocations()
    {
        return Ok(await _locationService.GetLocationAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> GetLocationById(int id)
    {
        var entity = await _locationService.GetLocationByIdAsync(id);
        if(entity == null)
            return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> AddLocation([FromBody] CreateLocationDTO dto)
    {
        var createdEntity = await _locationService.CreateLocationAsync(dto);
        return CreatedAtAction(nameof(GetLocationById), new { id = createdEntity.Id }, createdEntity);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> UpdateLocation(int id, [FromBody] CreateLocationDTO dto)
    {
        var entity = await _locationService.UpdateLocationAsync(id, dto);
        if(entity == false)
            return NotFound();
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Администратор,Менеджер мероприятий")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var isExist = await _locationService.DeleteLocationAsync(id);
        if(isExist == false)
            return NotFound();
        return NoContent();
    }
}