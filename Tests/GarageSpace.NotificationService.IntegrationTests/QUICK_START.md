# Quick Start Guide - Integration Tests

## Quick Setup (5 minutes)

### Step 1: Configure Email Settings

Edit `appsettings.Test.json` and add your SMTP credentials:

```json
{
  "Email": {
    "SmtpHost": "smtp.gmail.com",
    "SmtpPort": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "EnableSsl": true,
    "FromAddress": "your-email@gmail.com",
    "FromName": "MyGarage Test"
  },
  "TestEmail": {
    "RecipientEmail": "test-recipient@example.com"
  }
}
```

**For Gmail:** Use an [App Password](https://myaccount.google.com/apppasswords), not your regular password.

### Step 2: Enable Real Email Tests

Open `Integration/EmailServiceIntegrationTests.cs` and remove `Skip = "..."` from the test methods you want to run, or comment out the `[Fact(Skip = "...")]` line.

### Step 3: Run Tests

```bash
# From solution root
dotnet test tests/TaranSoft.MyGarage.NotificationService.Tests/ --filter "Category=Integration"
```

### Step 4: Check Your Email

Check the recipient email inbox to verify the test email was sent successfully.

## Alternative: Use User Secrets (More Secure)

```bash
cd tests/TaranSoft.MyGarage.NotificationService.Tests
dotnet user-secrets init
dotnet user-secrets set "Email:Username" "your-email@gmail.com"
dotnet user-secrets set "Email:Password" "your-app-password"
dotnet user-secrets set "TestEmail:RecipientEmail" "recipient@example.com"
```

## What Gets Tested?

1. **EmailServiceIntegrationTests**: Tests the `IEmailService` directly
   - Sends HTML emails
   - Sends plain text emails
   - Validates email format
   - Validates required parameters

2. **NotificationServiceIntegrationTests**: Tests the full notification flow
   - Requires `GetUserEmailAsync` to be implemented

## Troubleshooting

- **"Authentication failed"**: Check your credentials, use App Password for Gmail
- **"Connection timeout"**: Verify SMTP host and port
- **Tests skipped**: Remove `Skip` attribute from `[Fact]` attributes
- **Email not received**: Check spam folder, verify recipient email

For detailed setup, see [INTEGRATION_TEST_SETUP.md](./INTEGRATION_TEST_SETUP.md)
