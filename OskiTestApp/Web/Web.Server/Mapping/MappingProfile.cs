using AutoMapper;
using Web.Server.Models;
using Web.Server.Models.Dtos;
using Web.Server.ViewModels;

namespace Web.Server.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginDto, LoginViewModel>().ReverseMap();
        CreateMap<UserDto, UserViewModel>().ReverseMap();
    }
}