using AutoMapper;
using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.EventMember;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Interfaces;
using KaznacheystvoCalendar.Interfaces.ISevices;
using KaznacheystvoCalendar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KaznacheystvoCalendar.Services;

public class EventMemberService:IEventMemberService
{
    private readonly IGenericRepository<EventMember> _evntMemberRepo;
    private readonly IMapper _mapper;

    public EventMemberService(IGenericRepository<EventMember> evntMemberRepo, IMapper mapper)
    {
        _evntMemberRepo = evntMemberRepo;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<GetAllEventMemberDTO>> GetEventMembers(QueryObject query, int id)
    {
        var members = _evntMemberRepo.GetQueryable()
            .Include(x=> x.User)
            .Include(x=> x.Event);
        
        if (!string.IsNullOrEmpty(query.Search))
        {
            // Применяем фильтрацию по всем полям, которые вы хотите включить в поиск
            var searchLower = query.Search.ToLower();
            members = (IIncludableQueryable<EventMember, Event>)members.Where(r =>
                r.Id.ToString().ToLower().Contains(searchLower) ||
                r.User.FullName.ToLower().Contains(searchLower)
            );
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        
        var items = await members
            .Skip(skipNumber)
            .Take(query.PageSize)
            .ToListAsync();
        
        var totalCount = await members.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        
        var result = new PaginatedResponse<GetAllEventMemberDTO>
        {
            TotalCount = totalCount,
            TotalPages = totalPages,
            Items = _mapper.Map<List<GetAllEventMemberDTO>>(items)
        };
        
        return result;
    }

    public async Task<CreatedEventMemberDTO> CreateEventMember(CreateEventMemberDTO dto)
    {
        var isRegistered = await _evntMemberRepo.GetQueryable().FirstOrDefaultAsync(x => x.UserId == dto.userId && x.EventId == dto.eventId);
        if (isRegistered != null)
            return null;
        return _mapper.Map<CreatedEventMemberDTO>(await _evntMemberRepo.CreateAsync(_mapper.Map<EventMember>(dto)));
    }

    public async Task<bool> DeleteEventMember(int id,int userId)
    {
        var isExist = await _evntMemberRepo.GetQueryable().Where(x => x.EventId == id && x.UserId == userId).FirstOrDefaultAsync();
        if(isExist == null)
            return false;
        await _evntMemberRepo.DeleteAsync(isExist);
        return true;
    }
}