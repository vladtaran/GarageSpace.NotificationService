namespace GarageSpace.NotificationService.Events;

public class NewSubscriberCreated
{
    public Guid UserId { get; init; }
    public Guid FollowedUserId { get; init; }
    public DateTime Timestamp { get; init; }
} 