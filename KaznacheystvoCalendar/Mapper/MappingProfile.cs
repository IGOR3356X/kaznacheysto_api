using AutoMapper;
using CoolFormApi.DTO.Auth;
using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.DTO.Event;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User Mappers
        CreateMap<CreateUserDTO, User>();
        CreateMap<User,GetAllUserDTO> ();
        CreateMap<User,GetUserDTO>().ForMember(act => act.Role, opt => opt
            .MapFrom(src => src.Role.Name))
            .ForMember(act => act.Department, opt => opt
                .MapFrom(src => src.Department.Name));
        CreateMap<UpdateUserDTO, User>();
        
        //Event Mappers
        CreateMap<Event, EventViewDTO>().ForMember(act => act.Location, 
            opt => opt
                .MapFrom(src => src.Location.Name))
            .ForMember(act => act.EventName, opt => opt
                .MapFrom(src => src.Name));
        
        //EventVisibility Mappers
        CreateMap<EventVisible, EventViewDTO>().ForMember(act => act.Description, opt => opt
            .MapFrom(scr=> scr.Event.Description))
            .ForMember(scr => scr.Location, opt => opt
            .MapFrom(scr => scr.Event.Location.Name))
            .ForMember(act => act.StartDateTime, opt => opt
            .MapFrom(src => src.Event.StartDateTime))
            .ForMember(scr => scr.EndDateTime, opt => opt
            .MapFrom(src => src.Event.EndDateTime));
        
        //Comments Mappers
        CreateMap<Comment, CommentDTO>().ForMember(act => act.UserName, opt => opt
            .MapFrom(src => src.User.FullName));
        CreateMap<CreateCommentDTO, Comment>().ForMember(src => src.DateTime, opt => opt
            .MapFrom(src => DateTime.Now));
        CreateMap<Comment, CreateCommentDTO>();
        CreateMap<Comment, CreatedCommentDTO>().ForMember(src => src.Date, opt => opt
            .MapFrom(src => src.DateTime));
    }
}