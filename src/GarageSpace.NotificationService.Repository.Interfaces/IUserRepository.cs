using GarageSpace.NotificationService.Models.Domain;

namespace GarageSpace.NotificationService.Repository.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created user with updated properties.</returns>
    Task<UserEntity> CreateAsync(UserEntity user);

    /// <summary>
    /// Get a user in the database by user id.
    /// </summary>
    /// <param name="id">The user id to get.</param>
    /// <returns>The searched user object with properties.</returns>
    public Task<UserEntity?> GetUserById(long id);

} 