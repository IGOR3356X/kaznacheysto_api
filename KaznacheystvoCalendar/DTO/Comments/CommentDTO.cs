namespace KaznacheystvoCalendar.DTO;

public class CommentDTO
{
    public string UserName { get; set; }
    
    public DateTime DateTime { get; set; }

    public string Text { get; set; } = null!;
}