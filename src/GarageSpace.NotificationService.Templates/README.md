# GarageSpace.NotificationService.Templates

This project contains Razor email templates for the notification service.

## Structure

```
GarageSpace.NotificationService.Templates/
├── Models/                          # Template view models
│   └── NewFollowerEmailModel.cs
├── Templates/                       # Razor email templates
│   ├── _EmailLayout.cshtml         # Base email layout
│   ├── _ViewImports.cshtml        # Razor imports
│   └── NewFollowerEmail.cshtml    # New follower email template
└── Services/                       # Template rendering services
    ├── IEmailTemplateService.cs
    └── RazorEmailTemplateService.cs
```

## Usage

```csharp
// Register the service
builder.Services.AddScoped<IEmailTemplateService, RazorEmailTemplateService>();

// Use in NotificationService
var model = new NewFollowerEmailModel
{
    UserId = followerEvent.UserId,
    FollowedUserId = followerEvent.FollowedUserId,
    Timestamp = followerEvent.Timestamp,
    AppName = "GarageSpace"
};

var emailBody = await _templateService.RenderTemplateAsync("NewFollowerEmail", model);
var subject = _templateService.GetSubject("NewFollowerEmail");
```

## Adding New Templates

1. Create a new model in `Models/` folder
2. Create a new `.cshtml` template in `Templates/` folder
3. Add the subject to `TemplateSubjects` dictionary in `RazorEmailTemplateService.cs`
