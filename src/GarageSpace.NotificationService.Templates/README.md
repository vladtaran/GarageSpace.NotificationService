# GarageSpace.NotificationService.Templates

This project contains Razor email templates for the notification service.

## Structure

```
GarageSpace.NotificationService.Templates/
├── Models/                          # Template view models
│   └── NewSubscriberEmailModel.cs
├── Templates/                       # Razor email templates
│   ├── _EmailLayout.cshtml         # Base email layout
│   ├── _ViewImports.cshtml        # Razor imports
│   └── NewSubscriberEmail.cshtml    # New subscriber email template
└── Services/                       # Template rendering services
    ├── IEmailTemplateService.cs
    └── RazorEmailTemplateService.cs
```

## Usage

```csharp
// Register the service
builder.Services.AddScoped<IEmailTemplateService, RazorEmailTemplateService>();

// Use in NotificationService
var model = new NewSubscriberEmailModel
{
    UserId = subscriberEvent.UserId,
    FollowedUserId = subscriberEvent.FollowedUserId,
    Timestamp = subscriberEvent.Timestamp,
    AppName = "GarageSpace"
};

var emailBody = await _templateService.RenderTemplateAsync("NewSubscriberEmail", model);
var subject = _templateService.GetSubject("NewSubscriberEmail");
```

## Adding New Templates

1. Create a new model in `Models/` folder
2. Create a new `.cshtml` template in `Templates/` folder
3. Add the subject to `TemplateSubjects` dictionary in `RazorEmailTemplateService.cs`
