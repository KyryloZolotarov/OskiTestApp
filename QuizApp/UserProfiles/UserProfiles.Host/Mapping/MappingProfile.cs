using AutoMapper;
using UserProfiles.Host.Data.Entities;
using UserProfiles.Host.Models.Dtos;

namespace UserProfiles.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, UserEntity>().ReverseMap();
    }
}