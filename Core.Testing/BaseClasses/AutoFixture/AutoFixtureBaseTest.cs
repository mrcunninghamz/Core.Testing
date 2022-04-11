using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Community.AutoMapper;
using AutoMapper;
using Xunit;

namespace Core.Testing.BaseClasses.AutoFixture;

public abstract class BaseTest<TSubject> where TSubject : class
{
    protected IMapper Mapper { get; }
        
    protected TSubject TestSubject { get; }
        
    protected Fixture Fixture { get; }

    protected BaseTest()
    {
        Fixture = new Fixture();
        Fixture.Customize(new CompositeCustomization(new AutoMoqCustomization(), 
            new AutoMapperCustomization(x => x.AddMaps(typeof(TSubject))))
        );
        SetupTest();
        Mapper = Fixture.Create<IMapper>();
        TestSubject = Fixture.Create<TSubject>();
    }

    protected abstract void ConfigureFixture();

    private void SetupTest()
    {
        ConfigureFixture();
    }

    [Fact]
    public void TestMaps() => Mapper.ConfigurationProvider.AssertConfigurationIsValid();
}