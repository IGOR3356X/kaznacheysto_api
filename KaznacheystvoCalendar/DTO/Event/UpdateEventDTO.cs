namespace KaznacheystvoCalendar.DTO.Event;

public class UpdateEventDTO
{
    public int ManagerId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDateTime { get; set; }
    
    public DateTime EndDateTime { get; set; }

    public int LocationId { get; set; }

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;
    public int[] DepartmentsId { get; set; }
}