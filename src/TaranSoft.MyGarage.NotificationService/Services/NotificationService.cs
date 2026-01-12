using TaranSoft.MyGarage.NotificationService.Events;

namespace TaranSoft.MyGarage.NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public async Task HandleNewFollowerCreatedNotificationAsync(NewFollowerCreated evt, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending new follower notification for user {FollowedUserId}", evt.FollowedUserId);

        try
        {
            await SendEmailNotificationAsync(evt, cancellationToken);
            
            _logger.LogInformation("Successfully sent all notifications for new follower event");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send new follower notification for user {FollowedUserId}", evt.FollowedUserId);
            throw;
        }
    }

    private async Task SendEmailNotificationAsync(NewFollowerCreated followerEvent, CancellationToken cancellationToken)
    {
        // TODO: Implement email notification logic
    }
} 