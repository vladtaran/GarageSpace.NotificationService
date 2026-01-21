namespace GarageSpace.NotificationService.Application.Interfaces
{
    public interface IEmailNotificationSender
    {
        Task SendEmailAsync(string email, EmailData data);
    }
}
