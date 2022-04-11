using AutoMapper;

namespace Core.Testing.Tests.Mocks;

public class SeedService
{
    private readonly IMapper _mapper;

    public SeedService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TestDto TestMethod(Test test)
    {
        return _mapper.Map<TestDto>(test);
    }
}