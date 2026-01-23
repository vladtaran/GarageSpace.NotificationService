using GarageSpace.Contracts;
using GarageSpace.NotificationService.Services.Interfaces;
using GarageSpace.NotificationService.Worker.Mappers;
using MassTransit;

namespace GarageSpace.NotificationService.Worker.Consumers;

public class SubscriberEventsConsumer : IConsumer<UserBlogFollowedEvent>
{
    private readonly ILogger<SubscriberEventsConsumer> _logger;
    private readonly INotificationService _notificationService;

    public SubscriberEventsConsumer(ILogger<SubscriberEventsConsumer> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<UserBlogFollowedEvent> context)
    {
        var message = context.Message;
        
        _logger.LogDebug("Consuming new subscriber event: SubscriberId={SubscriberId}, SubscribedUserId={SubscribedUserId}, Timestamp={Timestamp}", 
            message.UserId, message.FollowerUserId, message.OccurredAt);

        try
        {
            var userBlogFollower = UserFollowerMapper.MapFromUserBlogFollowedEvent(message);

            await _notificationService.HandleNewFollowerCreatedNotificationAsync(userBlogFollower, message.OccurredAt);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consuming new subscriber event: SubscriberId={SubscriberId}, SubscribedUserId={SubscribedUserId}", message.UserId, message.FollowerUserId);
            throw;
        }
    }
} 