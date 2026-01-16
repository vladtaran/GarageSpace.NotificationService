using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GarageSpace.NotificationService.Services;
using Xunit;

namespace GarageSpace.NotificationService.IntegrationTests;

public class EmailServiceIntegrationTests : IClassFixture<EmailTestFixture>
{
    private readonly EmailTestFixture _fixture;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public EmailServiceIntegrationTests(EmailTestFixture fixture)
    {
        _fixture = fixture;
        _emailService = fixture.EmailService;
        _configuration = fixture.Configuration;
    }

    [Fact]
    public async Task SendEmailAsync_WithValidConfiguration_ShouldSendEmailSuccessfully()
    {
        // Arrange
        var recipientEmail = _configuration["TestEmail:RecipientEmail"] 
            ?? throw new InvalidOperationException("TestEmail:RecipientEmail must be configured");
        
        var subject = $"Integration Test - {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";
        var body = $@"
            <html>
            <body>
                <h1>Email Service Integration Test</h1>
                <p>This is a test email sent from the integration test suite.</p>
                <p><strong>Timestamp:</strong> {DateTime.UtcNow:O}</p>
                <p>If you received this email, the integration test passed!</p>
            </body>
            </html>";

        // Act
        var act = async () => await _emailService.SendEmailAsync(
            recipientEmail,
            subject,
            body,
            isHtml: true);

        // Assert
        await act.Should().NotThrowAsync();
    }
}
