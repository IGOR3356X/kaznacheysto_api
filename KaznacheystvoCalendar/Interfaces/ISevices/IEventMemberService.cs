using KaznacheystvoCalendar.DTO.EventMember;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface IEventMemberService
{
    public Task<GetAllEventMembers> GetAllMembers();
    
}