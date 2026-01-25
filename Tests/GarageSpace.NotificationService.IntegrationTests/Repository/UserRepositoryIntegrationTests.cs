using Xunit;
using GarageSpace.NotificationService.Repository;
using GarageSpace.NotificationService.Models.Domain;
using GarageSpace.IntegrationTests;

namespace GarageSpace.NotificationService.IntegrationTests.Repository;

public class UserRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public UserRepositoryIntegrationTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        
    }

    [Fact]
    public async Task CreateAsync_Success()
    {
        // Arrange
        var user = CreateNewUser();

        await using var dbContext = _fixture.CreateDbContext();
        UserRepository repository = new UserRepository(dbContext);

        // Act
        await repository.CreateAsync(user);

        // Assert
        var createdUser = await repository.GetUserById(user.Id);
        Assert.NotNull(createdUser);
        Assert.Equal(user.Id, createdUser.Id);
    }

    private UserEntity CreateNewUser()
    {
        var user = new UserEntity
        {
            Name = "testName",
            Nickname = "testNickName",
            Email = "test.email@email.com"
        };

        return user;
    }

} 