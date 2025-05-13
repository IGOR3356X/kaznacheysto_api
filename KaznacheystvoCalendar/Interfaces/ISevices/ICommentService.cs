using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface ICommentService
{
    public Task<List<CommentDTO>> GetCommentsForEventAsync(int eventId);
    public Task<CreatedCommentDTO> CreateCommentAsync(CreateCommentDTO comment);
    public Task<bool> DeleteCommentAsync(int commentId);
    public Task<CommentDTO> GetCommentByIdAsync(int id);
}