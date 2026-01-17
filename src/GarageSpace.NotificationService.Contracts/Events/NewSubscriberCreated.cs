namespace GarageSpace.NotificationService.Events;

public class NewSubscriberCreated
{
    public Guid UserId { get; init; }
    public Guid SubscribedUserId { get; init; }
    public DateTime Timestamp { get; init; }
} 