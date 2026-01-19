using GarageSpace.Contracts;

namespace GarageSpace.NotificationService.Interfaces;

public interface INotificationService
{
    Task HandleNewSubscriberCreatedNotificationAsync(UserBlogFollowedEvent evt, CancellationToken cancellationToken = default);
} 