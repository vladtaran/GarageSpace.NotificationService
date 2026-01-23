using GarageSpace.NotificationService.Models;
using GarageSpace.NotificationService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GarageSpace.NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IEmailNotificationSender _emailNotificationSender;

    public NotificationService(
        ILogger<NotificationService> logger,
        IEmailNotificationSender emailNotificationSender)
    {
        _logger = logger;
        _emailNotificationSender = emailNotificationSender;
    }

    public async Task HandleNewFollowerCreatedNotificationAsync(UserFollower follower, DateTime occuredAt)
    {
        _logger.LogInformation("Sending new subscriber notification for user {FollowedUserId}", follower.FollowerUserId);

        try
        {
            await SendEmailNotificationAsync(follower);

            _logger.LogInformation("Successfully sent all notifications for new subscriber event");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send new subscriber notification for user {FollowedUserId}", follower.FollowerUserId);
            throw;
        }
    }

    private async Task SendEmailNotificationAsync(UserFollower follower)
    {
        var recipientEmail = await GetUserEmailAsync(follower.UserId);

        if (string.IsNullOrWhiteSpace(recipientEmail))
        {
            _logger.LogWarning("Cannot send email notification: No email address found for user {FollowedUserId}", follower.FollowerUserId);
            return;
        }

        try
        {
            await _emailNotificationSender.SendEmailAsync(recipientEmail, new EmailData());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to send email notification to {RecipientEmail} for user {FollowedUserId}", recipientEmail, follower.FollowerUserId);
            throw;
        }
    }

    private async Task<string?> GetUserEmailAsync(long userId)
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