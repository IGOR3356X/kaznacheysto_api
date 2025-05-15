using KaznacheystvoCalendar.DTO.Departament;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.DTO.Event;

public class ViewEventByIdDTO
{
    public int Id { get; set; }

    public string Manager { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDateTime { get; set; }

    public string Location { get; set; }

    public string Description { get; set; } = null!;

    public DateTime EndDateTime { get; set; }
    
    public List<GetDeparamentsDTO> Deparaments { get; set; }
    public List<CommentDTO> Comments { get; set; }

}