using AutoMapper;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
