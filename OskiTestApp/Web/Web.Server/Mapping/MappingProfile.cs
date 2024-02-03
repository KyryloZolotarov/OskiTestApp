using AutoMapper;
using Web.Server.Models.Dtos;
using Web.Server.ViewModels;

namespace Web.Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuestionDto, QuestionViewModel>().ReverseMap();
        }
    }
}
