using MassTransit;
using GarageSpace.NotificationService.Events;
using GarageSpace.NotificationService.Interfaces;

namespace GarageSpace.NotificationService.Consumers;

public class NewFollowerCreatedConsumer : IConsumer<NewSubscriberCreated>
{
    private readonly ILogger<NewFollowerCreatedConsumer> _logger;
    private readonly INotificationService _notificationService;

    public NewFollowerCreatedConsumer(ILogger<NewFollowerCreatedConsumer> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<NewSubscriberCreated> context)
    {
        var message = context.Message;
        
        _logger.LogDebug("Consuming new follower event: FollowerId={FollowerId}, FollowedUserId={FollowedUserId}, Timestamp={Timestamp}", 
            message.UserId, message.FollowedUserId, message.Timestamp);

        try
        {
            await _notificationService.HandleNewSubscriberCreatedNotificationAsync(message, context.CancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consuming new follower event: FollowerId={FollowerId}, FollowedUserId={FollowedUserId}", message.UserId, message.FollowedUserId);
            throw;
        }
    }
} 