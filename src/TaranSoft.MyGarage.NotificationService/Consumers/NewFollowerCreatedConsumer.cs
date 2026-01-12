using MassTransit;
using TaranSoft.MyGarage.NotificationService.Events;
using TaranSoft.MyGarage.NotificationService.Services;

namespace TaranSoft.MyGarage.NotificationService.Consumers;

public class NewFollowerCreatedConsumer : IConsumer<NewFollowerCreated>
{
    private readonly ILogger<NewFollowerCreatedConsumer> _logger;
    private readonly INotificationService _notificationService;

    public NewFollowerCreatedConsumer(ILogger<NewFollowerCreatedConsumer> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<NewFollowerCreated> context)
    {
        var message = context.Message;
        
        _logger.LogDebug("Consuming new follower event: FollowerId={FollowerId}, FollowedUserId={FollowedUserId}, Timestamp={Timestamp}", 
            message.UserId, message.FollowedUserId, message.Timestamp);

        try
        {
            await _notificationService.HandleNewFollowerCreatedNotificationAsync(message, context.CancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consuming new follower event: FollowerId={FollowerId}, FollowedUserId={FollowedUserId}", message.UserId, message.FollowedUserId);
            throw;
        }
    }
} 