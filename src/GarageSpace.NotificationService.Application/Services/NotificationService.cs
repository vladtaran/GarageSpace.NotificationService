using GarageSpace.Contracts;
using GarageSpace.NotificationService.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GarageSpace.NotificationService.Application.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IEmailNotificationSender _emailNotificationSender;
    private readonly IConfiguration _configuration;

    public NotificationService(
        ILogger<NotificationService> logger,
        IEmailNotificationSender emailNotificationSender,
        IConfiguration configuration)
    {
        _logger = logger;
        _emailNotificationSender = emailNotificationSender;
        _configuration = configuration;
    }

    public async Task HandleNewSubscriberCreatedNotificationAsync(UserBlogFollowedEvent evt, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending new subscriber notification for user {FollowedUserId}", evt.FollowerUserId);

        try
        {
            await SendEmailNotificationAsync(evt, cancellationToken);

            _logger.LogInformation("Successfully sent all notifications for new subscriber event");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send new subscriber notification for user {FollowedUserId}", evt.FollowerUserId);
            throw;
        }
    }

    private async Task SendEmailNotificationAsync(UserBlogFollowedEvent subscriberEvent, CancellationToken cancellationToken)
    {
        var recipientEmail = await GetUserEmailAsync(subscriberEvent.FollowerUserId, cancellationToken);

        if (string.IsNullOrWhiteSpace(recipientEmail))
        {
            _logger.LogWarning("Cannot send email notification: No email address found for user {FollowedUserId}", subscriberEvent.FollowerUserId);
            return;
        }

        try
        {
            await _emailNotificationSender.SendEmailAsync(recipientEmail, new EmailData());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to send email notification to {RecipientEmail} for user {FollowedUserId}", recipientEmail, subscriberEvent.FollowerUserId);
            throw;
        }
    }

    private async Task<string?> GetUserEmailAsync(long userId, CancellationToken cancellationToken)
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
}