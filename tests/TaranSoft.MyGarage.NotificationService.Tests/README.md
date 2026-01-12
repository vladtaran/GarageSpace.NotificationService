# Unit Tests

This project contains unit tests for the TaranSoft.MyGarage.NotificationService.

## Test Structure

```
TaranSoft.MyGarage.NotificationService.Tests/
├── Consumers/
│   └── NewFollowerEventConsumerTests.cs
├── Services/
│   └── NotificationServiceTests.cs
└── TaranSoft.MyGarage.NotificationService.Tests.csproj
```

## Test Dependencies

- xUnit
- Moq
- FluentAssertions
- Microsoft.NET.Test.Sdk

## Running Tests

```bash
dotnet test tests/TaranSoft.MyGarage.NotificationService.Tests/
``` 