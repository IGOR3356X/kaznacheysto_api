using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.Interfaces.ISevices;
using KaznacheystvoCalendar.Models;
using Microsoft.AspNetCore.Mvc;

namespace KaznacheystvoCalendar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO comment)
    {
        var gg = await _commentService.CreateCommentAsync(comment);
        return CreatedAtAction(nameof(GetCommentById),new{id = gg.Id},gg);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        if(await _commentService.DeleteCommentAsync(id) == false)
            return NotFound();
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        var isExist = await _commentService.GetCommentByIdAsync(id);
        if (isExist == null)
        {
            return NotFound();
        }
        return Ok(isExist);
    }
}