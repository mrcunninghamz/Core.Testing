using System;
using Core.Testing.BaseClasses;
using Core.Testing.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Xunit;

namespace Core.Testing.Tests.BaseClasses;

public class BaseTestTests : BaseTest<BaseTestTests,BaseTestData>
{
    private Mock<ISeededTestValue> _seededValue;
    
    public BaseTestTests(BaseTestData fixture) : base(fixture)
    {
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        // Create mocks
        _seededValue = new Mock<ISeededTestValue>();
        
        // Add to dependency injection
        services.AddSingleton(_seededValue.Object);
        services.AddSingleton<SeededTestValueResolver>();
        
        //Run base configuration
        base.ConfigureServices(services);
    }

    [Fact]
    public void Mapper_ValidSetup_MapsSeed()
    {
        // Arrange
        var seed = Fixture.Seed;
        _seededValue.Setup(x => x.Value).Returns(seed);

        var test = Fixture.TestEntity;
        
        // Act
        var result = Mapper.Map<TestDto>(test);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(seed, result.Random);
    }
}

public class BaseTestData
{
    public Test TestEntity = new Test
    {
        Name = "Mapper Test"
    };
    
    public string Seed = Guid.NewGuid().ToString();
}