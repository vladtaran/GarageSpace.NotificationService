using GarageSpace.NotificationService.Events;

namespace GarageSpace.NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public NotificationService(
        ILogger<NotificationService> logger,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _logger = logger;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task HandleNewFollowerCreatedNotificationAsync(NewFollowerCreated evt, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending new follower notification for user {FollowedUserId}", evt.FollowedUserId);

        try
        {
            await SendEmailNotificationAsync(evt, cancellationToken);
            
            _logger.LogInformation("Successfully sent all notifications for new follower event");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send new follower notification for user {FollowedUserId}", evt.FollowedUserId);
            throw;
        }
    }

    private async Task SendEmailNotificationAsync(NewFollowerCreated followerEvent, CancellationToken cancellationToken)
    {
        var recipientEmail = await GetUserEmailAsync(followerEvent.FollowedUserId, cancellationToken);
        
        if (string.IsNullOrWhiteSpace(recipientEmail))
        {
            _logger.LogWarning("Cannot send email notification: No email address found for user {FollowedUserId}", 
                followerEvent.FollowedUserId);
            return;
        }

        var emailSubject = "You have a new follower!";
        var emailBody = BuildEmailBody(followerEvent);

        _logger.LogInformation(
            "Sending email notification to {RecipientEmail} for user {FollowedUserId}",
            recipientEmail,
            followerEvent.FollowedUserId);

        try
        {
            await _emailService.SendEmailAsync(
                recipientEmail,
                emailSubject,
                emailBody,
                isHtml: true,
                cancellationToken);

            _logger.LogInformation(
                "Successfully sent email notification to {RecipientEmail} for user {FollowedUserId}",
                recipientEmail,
                followerEvent.FollowedUserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to send email notification to {RecipientEmail} for user {FollowedUserId}",
                recipientEmail,
                followerEvent.FollowedUserId);
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

    private string BuildEmailBody(NewFollowerCreated followerEvent)
    {
        // Best Practice: Use email templates (Razor, Handlebars, or simple string replacement)
        // For production, consider using a template engine or dedicated email template service
        
        var fromEmail = _configuration["Email:FromAddress"] ?? "noreply@mygarage.com";
        var appName = _configuration["Email:AppName"] ?? "MyGarage";

        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; }}
        .content {{ padding: 20px; background-color: #f9f9f9; }}
        .footer {{ text-align: center; padding: 20px; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>{appName}</h1>
        </div>
        <div class=""content"">
            <h2>You have a new follower!</h2>
            <p>Great news! Someone just started following you on {appName}.</p>
            <p><strong>Follower ID:</strong> {followerEvent.UserId}</p>
            <p><strong>Date:</strong> {followerEvent.Timestamp:MMMM dd, yyyy 'at' HH:mm}</p>
            <p>Thank you for being part of our community!</p>
        </div>
        <div class=""footer"">
            <p>This is an automated message from {appName}. Please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>";
    }
} 