using MassTransit;
using GarageSpace.NotificationService.Events;
using GarageSpace.NotificationService.Interfaces;

namespace GarageSpace.NotificationService.Consumers;

public class NewSubscriberCreatedConsumer : IConsumer<NewSubscriberCreated>
{
    private readonly ILogger<NewSubscriberCreatedConsumer> _logger;
    private readonly INotificationService _notificationService;

    public NewSubscriberCreatedConsumer(ILogger<NewSubscriberCreatedConsumer> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<NewSubscriberCreated> context)
    {
        var message = context.Message;
        
        _logger.LogDebug("Consuming new subscriber event: SubscriberId={SubscriberId}, SubscribedUserId={SubscribedUserId}, Timestamp={Timestamp}", 
            message.UserId, message.SubscribedUserId, message.Timestamp);

        try
        {
            await _notificationService.HandleNewSubscriberCreatedNotificationAsync(message, context.CancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consuming new subscriber event: SubscriberId={SubscriberId}, SubscribedUserId={SubscribedUserId}", message.UserId, message.SubscribedUserId);
            throw;
        }
    }
} 