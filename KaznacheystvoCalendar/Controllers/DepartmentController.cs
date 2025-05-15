using KaznacheystvoCalendar.DTO.Departament;
using KaznacheystvoCalendar.Interfaces.ISevices;
using Microsoft.AspNetCore.Mvc;

namespace KaznacheystvoCalendar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController:ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartment()
    {
        return Ok(await _departmentService.GetAllDepartmentAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var entity = await _departmentService.GetDepartmentByIdAsync(id);
        if(entity == null)
            return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(CreateDepartamentDTO dto)
    {
        var entity = await _departmentService.CreateDepartmentAsync(dto);
        return CreatedAtAction(nameof(GetDepartmentById),new {id = entity.Id}, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, CreateDepartamentDTO dto)
    {
        var entity = await _departmentService.UpdateDepartmentAsync(id, dto);
        if(entity == null)
            return NotFound();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var entity = await _departmentService.DeleteDepartmentAsync(id);
        if (entity == null)
            return NotFound();
        return NoContent();
    }
}