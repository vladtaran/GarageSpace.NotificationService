namespace TaranSoft.MyGarage.NotificationService.Events;

public class NewFollowerCreated
{
    public Guid UserId { get; }
    public Guid FollowedUserId { get; }
    public DateTime Timestamp { get; }
} 