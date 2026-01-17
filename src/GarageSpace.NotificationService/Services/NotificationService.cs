using GarageSpace.NotificationService.Events;
using GarageSpace.NotificationService.Interfaces;
using GarageSpace.NotificationService.Templates.Models;

namespace GarageSpace.NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateRendererService _emailTemplateRendererService;
    private readonly IConfiguration _configuration;

    public NotificationService(
        ILogger<NotificationService> logger,
        IEmailService emailService,
        IEmailTemplateRendererService emailTemplateRendererService,
        IConfiguration configuration)
    {
        _logger = logger;
        _emailService = emailService;
        _emailTemplateRendererService = emailTemplateRendererService;
        _configuration = configuration;
    }

    public async Task HandleNewSubscriberCreatedNotificationAsync(NewSubscriberCreated evt, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending new subscriber notification for user {FollowedUserId}", evt.SubscribedUserId);

        try
        {
            await SendEmailNotificationAsync(evt, cancellationToken);

            _logger.LogInformation("Successfully sent all notifications for new subscriber event");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send new subscriber notification for user {FollowedUserId}", evt.SubscribedUserId);
            throw;
        }
    }

    private async Task SendEmailNotificationAsync(NewSubscriberCreated subscriberEvent, CancellationToken cancellationToken)
    {
        var recipientEmail = await GetUserEmailAsync(subscriberEvent.SubscribedUserId, cancellationToken);

        if (string.IsNullOrWhiteSpace(recipientEmail))
        {
            _logger.LogWarning("Cannot send email notification: No email address found for user {FollowedUserId}", subscriberEvent.SubscribedUserId);
            return;
        }

        var emailSubject = "You have a new subscriber!";
        var emailBody = await BuildEmailBodyAsync(subscriberEvent);

        _logger.LogInformation("Sending email notification to {RecipientEmail} for user {FollowedUserId}", recipientEmail, subscriberEvent.SubscribedUserId);

        try
        {
            await _emailService.SendEmailAsync(recipientEmail, emailSubject, emailBody, isHtml: true, cancellationToken);

            _logger.LogInformation("Successfully sent email notification to {RecipientEmail} for user {FollowedUserId}", recipientEmail, subscriberEvent.SubscribedUserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to send email notification to {RecipientEmail} for user {FollowedUserId}", recipientEmail, subscriberEvent.SubscribedUserId);
            throw;
        }
    }

    private async Task<string?> GetUserEmailAsync(Guid userId, CancellationToken cancellationToken)
    {
        // Best Practice: This should call your user service or database
        // For now, returning null - implement based on your architecture
        // Example: return await _userService.GetEmailByIdAsync(userId, cancellationToken);

        _logger.LogDebug("Retrieving email address for user {UserId}", userId);

        // TODO: Implement actual user email retrieval
        // This is a placeholder - replace with actual implementation
        await Task.CompletedTask;
        return null;
    }

    private async Task<string> BuildEmailBodyAsync(NewSubscriberCreated subscriberEvent)
    {
        NewSubscriberEmailModel testEmailModel = new NewSubscriberEmailModel 
        {
            UserId = subscriberEvent.UserId,
            SubscribedUserId = subscriberEvent.SubscribedUserId,
            Timestamp = subscriberEvent.Timestamp
        };

        return await _emailTemplateRendererService.RenderTemplateAsync("Email/NewSubscriberEmail", testEmailModel);
    }
}