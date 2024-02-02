using AutoMapper;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Dtos;

namespace TestCatalog.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuestionDto, QuestionEntity>().ReverseMap();
            CreateMap<TestDto, TestEntity>().ReverseMap();
        }
    }
}
