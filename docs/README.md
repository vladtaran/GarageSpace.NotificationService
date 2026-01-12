# TaranSoft.MyGarage.NotificationService

## Project Structure

This solution follows a standard .NET solution structure with clear separation of concerns:

```
TaranSoft.MyGarage.NotificationService/
├── src/                                    # Source code projects
│   └── TaranSoft.MyGarage.NotificationService/  # Main worker service
│       ├── Consumers/                      # MassTransit message consumers
│       ├── Events/                         # Event interfaces
│       ├── Services/                       # Business logic services
│       └── Worker.cs                       # Background service
├── tests/                                  # Test projects
├── docs/                                   # Documentation
└── TaranSoft.MyGarage.NotificationService.sln
```

## Architecture

- **src/**: Contains all source code projects
  - `TaranSoft.MyGarage.NotificationService/`: Background worker service for handling notifications
    - `Consumers/`: MassTransit message consumers for processing events
    - `Events/`: Event interfaces and contracts
    - `Services/`: Business logic services for notification processing

- **tests/**: Contains all test projects (to be added)
  - Unit tests
  - Integration tests
  - End-to-end tests

- **docs/**: Contains project documentation
  - API documentation
  - Architecture diagrams
  - Setup guides

## Features

- **Event-Driven Architecture**: Uses MassTransit with RabbitMQ for message processing
- **NewFollowerEvent Consumer**: Processes new follower events and sends notifications
- **Modular Design**: Separated concerns with dedicated services for different notification types
- **Logging**: Comprehensive logging for monitoring and debugging

## Getting Started

1. Ensure you have .NET 8.0 SDK installed
2. Clone the repository
3. Navigate to the solution directory
4. Run `dotnet restore` to restore dependencies
5. Run `dotnet build` to build the solution
6. Ensure RabbitMQ is running (configured in appsettings.json)
7. Run `dotnet run --project src/TaranSoft.MyGarage.NotificationService` to start the service

## Configuration

The service requires RabbitMQ configuration in `appsettings.json`:

```json
{
  "RabbitMQ": {
    "Host": "rabbitmq",
    "Username": "guest",
    "Password": "guest"
  }
}
```

## Development

This is a background worker service built using .NET 8.0 and MassTransit for event processing. The service:

- Listens for `NewFollowerEvent` messages on RabbitMQ
- Processes events through the `NewFollowerEventConsumer`
- Sends notifications via the `NotificationService`
- Supports email, push, and in-app notifications (implementations to be added) 