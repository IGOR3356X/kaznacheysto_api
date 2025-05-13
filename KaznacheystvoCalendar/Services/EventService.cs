using AutoMapper;
using KaznacheystvoCalendar.DTO;
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
    private readonly IMapper _mapper;

    public EventService(IGenericRepository<Event> eventRepository, IMapper mapper,
        IGenericRepository<EventVisible> visibleRepository, IGenericRepository<Comment> repositoryComment)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _visibleRepository = visibleRepository;
        _commentRepository = repositoryComment;
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
        var commentDtos =  _mapper.Map<List<CommentDTO>>(comments);

        return new ViewEventByIdDTO()
        {
            Id = events.Id,
            Manager = events.Manager.FullName,
            Name = events.Name,
            StartDateTime = events.StartDateTime,
            Location = events.Location.Name,
            Description = events.Description,
            EndDateTime = events.EndDateTime,
            Comments = commentDtos
        };
    }

    public Task<CreateEventDTO> CreateEventAsync(CreateEventDTO eventDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateEventAsync(UpdateEventDTO eventDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEventAsync(int id)
    {
        throw new NotImplementedException();
    }
}