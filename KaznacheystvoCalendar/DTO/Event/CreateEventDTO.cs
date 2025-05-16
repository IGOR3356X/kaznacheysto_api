using KaznacheystvoCalendar.DTO.Departament;

namespace KaznacheystvoCalendar.DTO.Event;

public class CreateEventDTO
{
    public int ManagerId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDateTime { get; set; }
    
    public DateTime EndDateTime { get; set; }

    public int LocationId { get; set; }
    
    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int[]? DepartmentsId{ get; set; }
}

public class CreatedEventDTO
{
    public int Id { get; set; }
    public string Manager { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDateTime { get; set; }
    
    public DateTime EndDateTime { get; set; }
    public string Location { get; set; }

    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public List<GetDeparamentsDTO> DepartmentNames{ get; set; }
}