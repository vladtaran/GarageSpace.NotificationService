using GarageSpace.NotificationService.Models;

namespace GarageSpace.NotificationService.Services.Interfaces;

public interface INotificationService
{
    Task HandleNewFollowerCreatedNotificationAsync(UserFollower follower, DateTime occuredAt);
} 