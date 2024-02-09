using AutoMapper;
using UserTest.Host.Data.Entities;
using UserTest.Host.Models.Dtos;

namespace UserTest.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserTestDto, UserTestEntity>().ReverseMap();
    }
}