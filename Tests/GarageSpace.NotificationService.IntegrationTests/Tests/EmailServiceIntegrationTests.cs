using FluentAssertions;
using GarageSpace.NotificationService.Interfaces;
using GarageSpace.NotificationService.Templates.Models;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace GarageSpace.NotificationService.IntegrationTests.Tests;

public class EmailServiceIntegrationTests : IClassFixture<EmailTestFixture>
{
    private readonly EmailTestFixture _fixture;
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateRendererService _emailTemplateRendererService;
    private readonly IConfiguration _configuration;

    public EmailServiceIntegrationTests(EmailTestFixture fixture)
    {
        _fixture = fixture;
        _emailService = fixture.EmailService;
        _emailTemplateRendererService = fixture.EmailTemplateRendererService;
        _configuration = fixture.Configuration;
    }

    [Fact]
    public async Task SendEmailAsync_NewUserBlogFollower_ShouldSendSuccessfully()
    {
        // Arrange
        var recipientEmail = _configuration["TestEmail:RecipientEmail"] 
            ?? throw new InvalidOperationException("TestEmail:RecipientEmail must be configured");
        
        var subject = $"Integration Test - {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";

        NewSubscriberEmailModel testEmailModel = new NewSubscriberEmailModel { Timestamp = DateTime.UtcNow };

        string body = await _emailTemplateRendererService.RenderTemplateAsync("Email/NewSubscriberEmail", testEmailModel);

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
