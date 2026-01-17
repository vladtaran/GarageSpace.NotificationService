namespace GarageSpace.NotificationService.Templates.Models;

public class NewSubscriberEmailModel
{
    public Guid UserId { get; set; }
    public Guid SubscribedUserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string AppName { get; set; } = "GarageSpace";
    public string FromEmail { get; set; } = "noreply@garagespace.com";
}
