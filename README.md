# Core.Testing
A set of base test classes that take care of mocking / dependency injection / automapper


## Classes

### BaseTest
This is an abstract class used in unit tests class to automatically setup automapper and puts your mocks into dependency injection.

#### Setup
In your unit test class reference the class you are testing. In this example the `PermissionsController` is the class being tested.

```c#
public class PermissionsControllerTests : BaseTest<PermissionsController>
```

The `BaseTest` class will look for automapper profiles in the assembly the `PermissionsController` comes from.

Next, override the `ConfigureServices` method.

```c#
protected override void ConfigureServices(IServiceCollection services)
        {
            _mockExternalEntityManager = new Mock<IExternalEntityManager>();
            _microsoftGraphUserAdapterMock = new Mock<IMicrosoftGraphUserAdapter>();
            _mockLogger = new Mock<ILogger<PermissionsController>>();

            //these are mocks for the controller
            services.AddTransient(_ => _mockExternalEntityManager.Object);
            services.AddTransient(_ => _microsoftGraphUserAdapterMock.Object);
            services.AddTransient(_ => _mockLogger.Object);

            //this is stuff that AutoMapper cares about
            _httpcontext = new DefaultHttpContext();
            var _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(_httpcontext);

            services.AddSingleton(_httpContextAccessor.Object);
            services.AddSingleton<PagingResponseConverter<ExternalEntity, ExternalEntity>>();

            base.ConfigureServices(services);
        }
```

All the mocks added to the `services` will be injected into the test class and automapper (if it needs it).
The base class will then create an instance of the `PermissionsController` and make it available in the `TestSubject` property of the class.


An example test:
```c#
[Fact]
public async Task GetGroupsByUser_UserExists_ReturnsOK()
{
    //Arrange
    var groups = new List<string> { "GroupA", "GroupB", "GroupC" };
    var objectId = "0e14e273-4282-4e5f-a368-4b0a9f266be5";

    _microsoftGraphUserAdapterMock.Setup(x => x.GetAssignedGroupsAsync(It.IsAny<string>())).ReturnsAsync(groups);

    //Act
    var response = await TestSubject.GetGroupsByUser(objectId);

    //Assert
    Assert.IsType<ActionResult<GroupAssignmentResponse>>(response);
    Assert.NotNull(response.Value.Groups);
    Assert.True(response.Value.Groups.Count == 3);
}
```
#### Properties

- TestSubject
  - This is the class you are testing setup by the base class and the dependencies that were configured.
- Mapper
  - An instance of automapper setup by the base class and the dependencies that were configured.
- ServiceProvider
  - The service provider with all the dependencies.
