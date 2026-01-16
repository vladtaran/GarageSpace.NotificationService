# Integration Tests

This project contains integration tests for the TaranSoft.MyGarage.NotificationService that send **real emails**.

## Project Structure

```
TaranSoft.MyGarage.NotificationService.Tests/
├── Integration/
│   ├── EmailServiceIntegrationTests.cs      # Tests for IEmailService
│   ├── NotificationServiceIntegrationTests.cs # Tests for INotificationService
│   └── EmailTestFixture.cs                 # Test fixture with DI setup
├── appsettings.Test.json                   # Test configuration
├── QUICK_START.md                          # Quick setup guide (5 minutes)
└── INTEGRATION_TEST_SETUP.md               # Detailed setup guide
```

## Quick Start

See [QUICK_START.md](./QUICK_START.md) for a 5-minute setup guide.

## Detailed Setup

See [INTEGRATION_TEST_SETUP.md](./INTEGRATION_TEST_SETUP.md) for comprehensive setup instructions including:
- Configuration options (JSON, User Secrets, Environment Variables)
- SMTP provider setup (Gmail, SendGrid, Office 365, Custom)
- CI/CD integration
- Troubleshooting

## Running Tests

### Run All Integration Tests

```bash
dotnet test tests/TaranSoft.MyGarage.NotificationService.Tests/ --filter "Category=Integration"
```

### Run Specific Test

```bash
dotnet test tests/TaranSoft.MyGarage.NotificationService.Tests/ --filter "FullyQualifiedName~EmailServiceIntegrationTests.SendEmailAsync_WithValidConfiguration_ShouldSendEmailSuccessfully"
```

### Run with Verbose Output

```bash
dotnet test tests/TaranSoft.MyGarage.NotificationService.Tests/ --filter "Category=Integration" --logger "console;verbosity=detailed"
```

## Test Dependencies

- xUnit - Testing framework
- FluentAssertions - Assertion library
- Microsoft.Extensions.* - Dependency injection and configuration

## Security Notes

⚠️ **Never commit credentials to source control!**

- Use User Secrets for local development
- Use environment variables or secure secret management for CI/CD
- The `appsettings.Test.json` file should not contain real credentials

## Test Categories

- `Category=Integration` - Integration tests that may send real emails
- `Requires=SmtpConfiguration` - Tests that require valid SMTP configuration

## Next Steps

1. Configure your SMTP settings (see QUICK_START.md)
2. Enable tests by removing `Skip` attributes
3. Run tests and verify emails are received
4. Implement `GetUserEmailAsync` in NotificationService for full integration tests
