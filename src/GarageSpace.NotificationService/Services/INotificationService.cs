using GarageSpace.NotificationService.Consumers;
using GarageSpace.NotificationService.Events;

namespace GarageSpace.NotificationService.Services;

public interface INotificationService
{
    Task HandleNewFollowerCreatedNotificationAsync(NewFollowerCreated evt, CancellationToken cancellationToken = default);
} 