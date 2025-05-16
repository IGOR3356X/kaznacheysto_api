using AutoMapper;
using CoolFormApi.DTO.Auth;
using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.DTO.Departament;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.EventMember;
using KaznacheystvoCalendar.DTO.Location;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User Mappers
        CreateMap<CreateUserDTO, User>();
        CreateMap<User, GetAllUserDTO>();
        CreateMap<User, GetUserDTO>()
            .ForMember(act => act.Role, opt => opt
                .MapFrom(src => src.Role.Name))
            .ForMember(act => act.Department, opt => opt
                .MapFrom(src => src.Department.Name));
        CreateMap<UpdateUserDTO, User>();

        //Event Mappers
        CreateMap<Event, EventViewDTO>()
            .ForMember(act => act.Location, opt => opt
                .MapFrom(src => src.Location.Name))
            .ForMember(act => act.EventName, opt => opt
                .MapFrom(src => src.Name));
        CreateMap<CreateEventDTO, Event>();
        CreateMap<Event, CreatedEventDTO>().ForMember(src => src.Manager, opt => opt
                .MapFrom(src => src.Manager.FullName))
            .ForMember(src => src.Location, opt => opt
                .MapFrom(src => src.Location.Name));
        CreateMap<Event, ViewEventByIdDTO>().ForMember(act => act.Manager, opt => opt
                .MapFrom(src => src.Manager.FullName))
            .ForMember(src => src.Location, opt => opt
                .MapFrom(src => src.Location.Name));

        //EventVisibility Mappers
        CreateMap<EventVisible, EventViewDTO>()
            .ForMember(act => act.Description, opt => opt
                .MapFrom(scr => scr.Event.Description))
            .ForMember(scr => scr.Location, opt => opt
                .MapFrom(scr => scr.Event.Location.Name))
            .ForMember(act => act.StartDateTime, opt => opt
                .MapFrom(src => src.Event.StartDateTime))
            .ForMember(scr => scr.EndDateTime, opt => opt
                .MapFrom(src => src.Event.EndDateTime));
        CreateMap<EventVisible, GetDeparamentsDTO>().ForMember(src => src.Name, opt => opt
            .MapFrom(src => src.Department.Name));

        //Comments Mappers
        CreateMap<Comment, CommentDTO>()
            .ForMember(act => act.UserName, opt => opt
                .MapFrom(src => src.User.FullName));
        CreateMap<CreateCommentDTO, Comment>()
            .ForMember(src => src.DateTime, opt => opt
                .MapFrom(src => DateTime.Now));
        CreateMap<Comment, CreateCommentDTO>();
        CreateMap<Comment, CreatedCommentDTO>()
            .ForMember(src => src.Date, opt => opt
                .MapFrom(src => src.DateTime));
        CreateMap<UpdateEventDTO, Event>();

        //Department Mappers
        CreateMap<Department, GetDeparamentsDTO>();
        CreateMap<CreateDepartamentDTO, Department>();
        CreateMap<Department, CreatedDepartamentDTO>();
        
        //Location Mappers
        CreateMap<Location, GetLocationDTO>();
        CreateMap<CreateLocationDTO, Location>();
        CreateMap<Location, CreatedLocationDTO>();
        
        //EventMember Mappers
        CreateMap<EventMember, GetAllEventMemberDTO>().ForMember(src => src.FullName, opt => opt
            .MapFrom(src => src.User.FullName));
        CreateMap<CreateEventMemberDTO, EventMember>();
        CreateMap<EventMember, CreatedEventMemberDTO>();
    }
}