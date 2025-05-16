namespace KaznacheystvoCalendar.DTO.EventMember;

public class CreateEventMemberDTO
{
    public int userId { get; set; }
    public int eventId { get; set; }
}

public class CreatedEventMemberDTO
{
    public int Id { get; set; }
    public int userId { get; set; }
    public int eventId { get; set; }
}