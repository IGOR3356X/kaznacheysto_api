using System.ComponentModel.DataAnnotations.Schema;

namespace KaznacheystvoCalendar.DTO;

public class CreateCommentDTO
{
    public int? EventId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
}
public class CreatedCommentDTO
{
    public int Id { get; set; }
    public int? EventId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime Date { get; set; }
}