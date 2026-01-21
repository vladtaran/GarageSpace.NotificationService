using GarageSpace.Contracts;

namespace GarageSpace.NotificationService.Application.Interfaces;

public interface INotificationService
{
    Task HandleNewSubscriberCreatedNotificationAsync(UserBlogFollowedEvent evt, CancellationToken cancellationToken = default);
} 