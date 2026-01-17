using GarageSpace.NotificationService.Events;

namespace GarageSpace.NotificationService.Interfaces;

public interface INotificationService
{
    Task HandleNewSubscriberCreatedNotificationAsync(NewSubscriberCreated evt, CancellationToken cancellationToken = default);
} 