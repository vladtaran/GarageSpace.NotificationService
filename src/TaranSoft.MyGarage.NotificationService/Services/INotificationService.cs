using TaranSoft.MyGarage.NotificationService.Consumers;
using TaranSoft.MyGarage.NotificationService.Events;

namespace TaranSoft.MyGarage.NotificationService.Services;

public interface INotificationService
{
    Task HandleNewFollowerCreatedNotificationAsync(NewFollowerCreated evt, CancellationToken cancellationToken = default);
} 