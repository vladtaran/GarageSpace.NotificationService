# Integration Test Setup Guide

This guide explains how to set up and run integration tests for the email notification service that send **real emails**.

## Prerequisites

1. .NET 8.0 SDK installed
2. A valid SMTP server configuration (Gmail, SendGrid, or your own SMTP server)
3. Test email account credentials

## Step 1: Configure Test Settings

### Option A: Using appsettings.Test.json (Recommended for Development)

1. Open `Tests/GarageSpace.NotificationService.IntegrationTests/appsettings.Test.json`
2. Update the email configuration with your SMTP settings:

```json
{
  "Email": {
    "SmtpHost": "smtp.gmail.com",
    "SmtpPort": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "EnableSsl": true,
    "FromAddress": "your-email@gmail.com",
    "FromName": "MyGarage Test",
    "TimeoutSeconds": 30,
    "AppName": "MyGarage Test"
  },
  "TestEmail": {
    "RecipientEmail": "recipient@example.com",
    "UseRealEmail": true
  }
}
```

**Important Notes:**
- For Gmail: You need to use an [App Password](https://support.google.com/accounts/answer/185833), not your regular password
- Never commit credentials to source control!

### Option B: Using User Secrets (Recommended for Local Development)

1. Navigate to the test project directory:
```bash
cd Tests/GarageSpace.NotificationService.IntegrationTests
```

2. Set up user secrets:
```bash
dotnet user-secrets init
dotnet user-secrets set "Email:Username" "your-email@gmail.com"
dotnet user-secrets set "Email:Password" "your-app-password"
dotnet user-secrets set "Email:FromAddress" "your-email@gmail.com"
dotnet user-secrets set "TestEmail:RecipientEmail" "recipient@example.com"
```

### Option C: Using Environment Variables (Recommended for CI/CD)

Set the following environment variables:

```bash
Email__Username=your-email@gmail.com
Email__Password=your-app-password
Email__FromAddress=your-email@gmail.com
Email__SmtpHost=smtp.gmail.com
Email__SmtpPort=587
Email__EnableSsl=true
TestEmail__RecipientEmail=recipient@example.com
```

## Step 2: Gmail Setup (If Using Gmail)

1. Enable 2-Factor Authentication on your Google account
2. Go to [Google App Passwords](https://myaccount.google.com/apppasswords)
3. Generate an app password for "Mail"
4. Use this app password (not your regular password) in the configuration

## Step 3: Alternative SMTP Providers

### SendGrid
```json
{
  "Email": {
    "SmtpHost": "smtp.sendgrid.net",
    "SmtpPort": 587,
    "Username": "apikey",
    "Password": "your-sendgrid-api-key",
    "EnableSsl": true
  }
}
```

### Outlook/Office 365
```json
{
  "Email": {
    "SmtpHost": "smtp.office365.com",
    "SmtpPort": 587,
    "Username": "your-email@outlook.com",
    "Password": "your-password",
    "EnableSsl": true
  }
}
```

### Custom SMTP Server
```json
{
  "Email": {
    "SmtpHost": "smtp.yourdomain.com",
    "SmtpPort": 587,
    "Username": "your-username",
    "Password": "your-password",
    "EnableSsl": true
  }
}
```

## Step 4: Run Integration Tests

### Run All Integration Tests

```bash
# From the solution root
dotnet test Tests/GarageSpace.NotificationService.IntegrationTests/ --filter "Category=Integration"
```

### Run Specific Integration Test

```bash
dotnet test Tests/GarageSpace.NotificationService.IntegrationTests/ --filter "FullyQualifiedName~EmailServiceIntegrationTests.SendEmailAsync_WithValidConfiguration_ShouldSendEmailSuccessfully"
```

### Run Tests with Verbose Output

```bash
dotnet test Tests/GarageSpace.NotificationService.IntegrationTests/ --filter "Category=Integration" --logger "console;verbosity=detailed"
```

## Step 5: Enable Real Email Tests

By default, integration tests that send real emails are **skipped** for safety. To enable them:

1. Remove the `Skip` attribute from the test methods, OR
2. Use a test filter to run only non-skipped tests:

```bash
# Run only enabled integration tests
dotnet test Tests/GarageSpace.NotificationService.IntegrationTests/ --filter "Category=Integration&FullyQualifiedName!~Skip"
```

Or modify the test file to remove `Skip = "..."` from the `[Fact]` attributes.

## Step 6: Verify Email Delivery

1. Check the recipient email inbox
2. Verify the email was received
3. Check the email content matches the test expectations
4. Review test logs for any errors

## Test Structure

```
Tests/GarageSpace.NotificationService.IntegrationTests/
├── EmailServiceIntegrationTests.cs           # Tests for IEmailService
├── NotificationServiceIntegrationTests.cs    # Tests for INotificationService
├── EmailTestFixture.cs                       # Test fixture with DI setup
├── appsettings.Test.json                    # Test configuration
└── INTEGRATION_TEST_SETUP.md                # This file
```

## Troubleshooting

### "Authentication failed" Error

- Verify your credentials are correct
- For Gmail: Make sure you're using an App Password, not your regular password
- Check that 2FA is enabled (required for App Passwords)

### "Connection timeout" Error

- Verify SMTP host and port are correct
- Check firewall settings
- Ensure SSL/TLS settings match your SMTP server requirements

### "Email not received"

- Check spam/junk folder
- Verify recipient email address is correct
- Check SMTP server logs if available
- Verify the test actually ran (check test output)

### Tests are Skipped

- Remove `Skip = "..."` from `[Fact]` attributes
- Or use test filters to run specific tests

## Best Practices

1. **Never commit credentials** - Use User Secrets or environment variables
2. **Use a dedicated test email account** - Don't use production email accounts
3. **Clean up test emails** - Consider adding cleanup logic if needed
4. **Rate limiting** - Be aware of SMTP server rate limits
5. **Test in CI/CD** - Use environment variables and secure secrets management

## CI/CD Integration

For CI/CD pipelines, use environment variables or secure secret management:

```yaml
# Example GitHub Actions
env:
  Email__Username: ${{ secrets.SMTP_USERNAME }}
  Email__Password: ${{ secrets.SMTP_PASSWORD }}
  Email__FromAddress: ${{ secrets.SMTP_FROM_ADDRESS }}
  TestEmail__RecipientEmail: ${{ secrets.TEST_RECIPIENT_EMAIL }}
```

## Next Steps

1. Implement `GetUserEmailAsync` in `NotificationService` to enable full integration tests
2. Add email template tests
3. Add retry logic tests
4. Add rate limiting tests
5. Set up automated email verification (checking inbox)
