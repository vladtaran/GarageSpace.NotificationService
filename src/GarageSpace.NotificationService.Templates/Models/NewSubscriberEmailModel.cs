namespace GarageSpace.NotificationService.Templates.Models;

public class NewSubscriberEmailModel
{
    public long UserId { get; set; }
    public long SubscribedUserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string AppName { get; set; } = "GarageSpace";
    public string FromEmail { get; set; } = "noreply@garagespace.com";
}
