using AutoMapper;

namespace Core.Testing.Tests.Mocks
{
    public class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            CreateMap<Test, TestDto>()
                .ForMember(dest => dest.Test, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Random, opt => opt.MapFrom<SeededTestValueResolver>());
        }
    }
}