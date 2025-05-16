using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.EventMember;
using KaznacheystvoCalendar.DTO.User;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface IEventMemberService
{
    public Task<PaginatedResponse<GetAllEventMemberDTO>> GetEventMembers(QueryObject queryObject, int id);
    public Task<CreatedEventMemberDTO> CreateEventMember(CreateEventMemberDTO dto);
    public Task<bool> DeleteEventMember(int id);
}