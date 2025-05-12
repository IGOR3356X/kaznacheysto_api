using AutoMapper;
using CoolFormApi.DTO.Auth;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDTO, User>();
        CreateMap<User,GetAllUserDTO> ();
        CreateMap<User,GetUserDTO>().ForMember(act => act.Role, opt => opt
            .MapFrom(src => src.Role.Name))
            .ForMember(act => act.Department, opt => opt
                .MapFrom(src => src.Department.Name));
        CreateMap<UpdateUserDTO, User>();
    }
}