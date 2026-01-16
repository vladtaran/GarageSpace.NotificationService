using MassTransit;
using GarageSpace.NotificationService;
using GarageSpace.NotificationService.Consumers;
using GarageSpace.NotificationService.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Best Practice: Configure email settings from configuration
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection(EmailSettings.SectionName));

// Best Practice: Register email service as singleton (SMTP client can be reused)
// For HTTP-based services (SendGrid, etc.), consider scoped or transient
builder.Services.AddSingleton<IEmailService, SmtpEmailService>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NewFollowerCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitConfig = builder.Configuration.GetSection("RabbitMQ");
        cfg.Host(rabbitConfig["Host"], "/", h =>
        {
            h.Username(rabbitConfig["Username"]);
            h.Password(rabbitConfig["Password"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();
