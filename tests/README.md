# Tests

This directory contains all test projects for the TaranSoft.MyGarage.NotificationService solution.

## Test Structure

When adding test projects, follow these naming conventions:

- `TaranSoft.MyGarage.NotificationService.Tests` - Unit tests
- `TaranSoft.MyGarage.NotificationService.IntegrationTests` - Integration tests
- `TaranSoft.MyGarage.NotificationService.FunctionalTests` - End-to-end tests

## Testing Guidelines

- Use xUnit as the testing framework
- Follow AAA pattern (Arrange, Act, Assert)
- Use descriptive test names that explain the scenario being tested
- Mock external dependencies in unit tests
- Use test containers for integration tests that require databases or external services

## Running Tests

```bash
# Run all tests
dotnet test

# Run tests for a specific project
dotnet test tests/TaranSoft.MyGarage.NotificationService.Tests/

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
``` 