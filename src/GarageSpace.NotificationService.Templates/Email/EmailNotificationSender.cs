using GarageSpace.NotificationService.Application.Interfaces;
using GarageSpace.NotificationService.Templates.Models;
using Microsoft.Extensions.Logging;

namespace GarageSpace.NotificationService.Templates.Email
{
    public class EmailNotificationSender : IEmailNotificationSender
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<IEmailNotificationSender> _logger;
        private readonly IEmailTemplateRendererService _emailTemplateRendererService;
        public EmailNotificationSender(IEmailService emailService) 
        {
            _emailService = emailService;
        }
        public async Task SendEmailAsync(string email, EmailData data)
        {
            var emailSubject = "You have a new subscriber!";
            var emailBody = await BuildEmailBodyAsync(data);

            _logger.LogInformation("Sending email notification to {RecipientEmail} for user {FollowedUserId}", email, data.FollowerUserId);

            try
            {
                await _emailService.SendEmailAsync(email, emailSubject, emailBody, isHtml: true);

                _logger.LogInformation("Successfully sent email notification to {RecipientEmail} for user {FollowedUserId}", email, data.FollowerUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to send email notification to {RecipientEmail} for user {FollowedUserId}", email, data.FollowerUserId);
                throw;
            }
        }

        private async Task<string> BuildEmailBodyAsync(EmailData data)
        {
            NewSubscriberEmailModel testEmailModel = new()
            {
                UserId = data.UserId,
                SubscribedUserId = data.FollowerUserId,
                Timestamp = data.OccurredAt
            };

            return await _emailTemplateRendererService.RenderTemplateAsync("Email/NewSubscriberEmail", testEmailModel);
        }
    }
}
