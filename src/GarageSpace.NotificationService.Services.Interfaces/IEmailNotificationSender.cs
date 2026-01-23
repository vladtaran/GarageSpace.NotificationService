using GarageSpace.NotificationService.Models;

namespace GarageSpace.NotificationService.Services.Interfaces
{
    public interface IEmailNotificationSender
    {
        Task SendEmailAsync(string email, EmailData data);
    }
}
