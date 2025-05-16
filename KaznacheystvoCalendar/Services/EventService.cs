using AutoMapper;
using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.DTO.Departament;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Interfaces;
using KaznacheystvoCalendar.Interfaces.ISevices;
using KaznacheystvoCalendar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KaznacheystvoCalendar.Services;

public class EventService : IEventService
{
    private readonly IGenericRepository<Event> _eventRepository;
    private readonly IGenericRepository<EventVisible> _visibleRepository;
    private readonly IGenericRepository<Comment> _commentRepository;
    private readonly IGenericRepository<Department> _departmentRepository;
    private readonly IMapper _mapper;

    public EventService(IGenericRepository<Event> eventRepository, IMapper mapper,
        IGenericRepository<EventVisible> visibleRepository, IGenericRepository<Comment> repositoryComment, IGenericRepository<Department> departmentRepository)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _visibleRepository = visibleRepository;
        _commentRepository = repositoryComment;
        _departmentRepository = departmentRepository;
    }

    public async Task<List<EventViewDTO>> GetCalendarEventsAsync(string userRole, string userDepartament, int month,
        int year)
    {
        if (userRole == "Администратор" || userRole == "Менеджер мероприятий")
        {
            var events = await _eventRepository.GetQueryable()
                .Include(x => x.Location)
                .Where(x => x.StartDateTime.Month == month && x.StartDateTime.Year == year)
                .ToListAsync();
            return _mapper.Map<List<EventViewDTO>>(events);
        }

        var userEvents = await _visibleRepository.GetQueryable()
            .Include(x => x.Event)
            .Include(x => x.Event.Location)
            .Where(x => x.Event.StartDateTime.Month == month
                        && x.Event.StartDateTime.Year == year
                        && x.Department.Name == userDepartament).ToListAsync();
        return _mapper.Map<List<EventViewDTO>>(userEvents);
    }

    public async Task<PaginatedResponse<EventViewDTO>> GetEventsAsync(QueryObject query, string userRole,
        string userDepartament)
    {
        if (userRole == "Администратор" || userRole == "Менеджер мероприятий")
        {
            var events = _eventRepository.GetQueryable()
                .Include(x => x.Location);

            if (!string.IsNullOrEmpty(query.Search))
            {
                var searchLower = query.Search.ToLower();
                events = (IIncludableQueryable<Event, Location>)events.Where(r =>
                    r.Id.ToString().ToLower().Contains(searchLower) ||
                    r.Name.ToLower().Contains(searchLower) ||
                    r.Location.Name.ToLower().Contains(searchLower) ||
                    r.StartDateTime.ToString().Contains(searchLower) ||
                    r.EndDateTime.ToString().Contains(searchLower) ||
                    r.Description.ToLower().Contains(searchLower)
                );
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            
            var totalCount = await events.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);

            var items = await events
                .OrderByDescending(x => x.StartDateTime)
                .Skip(skipNumber)
                .Take(query.PageSize).ToListAsync();
            
            var result = new PaginatedResponse<EventViewDTO>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = _mapper.Map<List<EventViewDTO>>(items)
            };

            return result;
        }
        else
        {
            var events =_visibleRepository.GetQueryable()
                .Include(x => x.Event)
                .Include(x => x.Event.Location)
                .Include(x => x.Department)
                .Where(x => x.Department.Name == userDepartament);

            if (!string.IsNullOrEmpty(query.Search))
            {
                var searchLower = query.Search.ToLower();
                events = events.Where(r =>
                    r.Id.ToString().ToLower().Contains(searchLower) ||
                    r.Event.Name.ToLower().Contains(searchLower) ||
                    r.Event.Location.Name.ToLower().Contains(searchLower) ||
                    r.Event.StartDateTime.ToString().Contains(searchLower) ||
                    r.Event.EndDateTime.ToString().Contains(searchLower) ||
                    r.Event.Description.ToLower().Contains(searchLower)
                );
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            var totalCount = events.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
            
            var items = await events
                .OrderByDescending(x => x.Event.StartDateTime)
                .Skip(skipNumber)
                .Take(query.PageSize).ToListAsync();

            var result = new PaginatedResponse<EventViewDTO>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = _mapper.Map<List<EventViewDTO>>(items)
            };

            return result;
        }
    }

    public async Task<ViewEventByIdDTO> GetEventByIdAsync(int id)
    {
        var events = await _eventRepository.GetQueryable()
            .Include(x => x.Location)
            .Include(x => x.Manager)
            .Where(x => x.Id == id).FirstOrDefaultAsync();
        
        var comments = await _commentRepository.GetQueryable()
            .Include(x => x.User).ToListAsync();
        
        var commentDtos = _mapper.Map<List<CommentDTO>>(comments);
        
        var listDepartments = await _visibleRepository.GetQueryable()
            .Include(x => x.Department)
            .Where(x => x.EventId == id).ToListAsync();
        
        var mappedDep = _mapper.Map<List<GetDeparamentsDTO>>(listDepartments);
        
        var mappedResult = _mapper.Map<ViewEventByIdDTO>(events);
        mappedResult.Comments = commentDtos;
        mappedResult.Deparaments = mappedDep;
        
        return mappedResult;
    }

    public async Task<CreatedEventDTO> CreateEventAsync(CreateEventDTO eventDto)
    {
        List<GetDeparamentsDTO> depNames = new List<GetDeparamentsDTO>();
        var createdEntity = await _eventRepository.CreateAsync(_mapper.Map<Event>(eventDto));
        var departments = eventDto.DepartmentsId.Select(
            t => new EventVisible() { EventId = createdEntity.Id, DepartmentId = t, }).ToList();

        var createdDep = await _visibleRepository.AddRangeAsync(departments);
        var listDep = createdDep.ToList();
        var finalEvent = await _eventRepository.GetQueryable()
            .Include(x => x.Location)
            .Include(x => x.Manager)
            .Where(x => x.Id == createdEntity.Id).FirstOrDefaultAsync();
        
        var outEntity = _mapper.Map<CreatedEventDTO>(finalEvent);
        foreach (var t in listDep)
        {
            var selectedDep = await _departmentRepository.GetByIdAsync(t.DepartmentId);
            var gg = new GetDeparamentsDTO()
            {
                Name = selectedDep.Name,
            };
            depNames.Add(gg);
        }
        
        outEntity.DepartmentNames = depNames;
        
        return outEntity;
    }

    public async Task<bool> UpdateEventAsync(int id,UpdateEventDTO eventDto)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        if(entity == null)
            return false;
        
        var updatedEntity = _mapper.Map(eventDto, entity);
        var selectedGG = await _visibleRepository.GetQueryable()
            .Include(x=> x.Department)
            .Where(x => x.EventId == updatedEntity.Id).ToListAsync();
        
        await _visibleRepository.DeleteRangeAsync(selectedGG);
        
        var departments = eventDto.departmentIds.Select(
            t => new EventVisible() { EventId = updatedEntity.Id, DepartmentId = t, }).ToList();
        
        await _visibleRepository.AddRangeAsync(departments);
        
        return true;
    }

    public Task<bool> DeleteEventAsync(int id)
    {
        throw new NotImplementedException();
    }
}