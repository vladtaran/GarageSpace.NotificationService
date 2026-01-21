namespace GarageSpace.NotificationService.Domain.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created user with updated properties.</returns>
    Task<User> CreateAsync(User user);
   
} 