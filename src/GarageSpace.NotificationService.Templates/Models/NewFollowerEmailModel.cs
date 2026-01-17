namespace GarageSpace.NotificationService.Templates.Models;

public class NewFollowerEmailModel
{
    public Guid UserId { get; set; }
    public Guid FollowedUserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string AppName { get; set; } = "GarageSpace";
    public string FromEmail { get; set; } = "noreply@garagespace.com";
}
