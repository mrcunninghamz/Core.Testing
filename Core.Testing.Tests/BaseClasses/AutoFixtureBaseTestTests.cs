using System;
using AutoFixture;
using Core.Testing.BaseClasses;
using Core.Testing.BaseClasses.AutoFixture;
using Core.Testing.Tests.Mocks;
using Moq;
using Xunit;

namespace Core.Testing.Tests.BaseClasses;

public class AutoFixtureBaseTestTests : Testing.BaseClasses.AutoFixture.BaseTest<SeedService>
{
    private Mock<ISeededTestValue> _seededValue;


    protected override void ConfigureFixture()
    {
        // Create mocks
        _seededValue = Fixture.Freeze<Mock<ISeededTestValue>>();
        Fixture.Freeze<SeededTestValueResolver>();
    }

    [Fact]
    public void Mapper_ValidSetup_MapsSeed()
    {
        // Arrange
        var seed = Guid.NewGuid().ToString();

        var test = Fixture.Create<Test>();

        _seededValue.Setup(x => x.Value).Returns(seed);
            
        // Act
        var result = TestSubject.TestMethod(test);
            
        // Assert
        Assert.NotNull(result);
        Assert.Equal(seed, result.Random);
    }
}