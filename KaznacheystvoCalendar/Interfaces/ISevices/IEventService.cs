using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.User;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface IEventService
{
    public Task<List<EventViewDTO>> GetCalendarEventsAsync(string userRole,string userDepartament, int month, int year);
    public Task<PaginatedResponse<EventViewDTO>> GetEventsAsync(QueryObject queryObject, string userRole,
        string userDepartament);
    public Task<ViewEventByIdDTO> GetEventByIdAsync(int id);
    public Task<CreatedEventDTO> CreateEventAsync(CreateEventDTO eventDto);
    public Task<bool> UpdateEventAsync(UpdateEventDTO eventDto);
    public Task<bool> DeleteEventAsync(int id);
}