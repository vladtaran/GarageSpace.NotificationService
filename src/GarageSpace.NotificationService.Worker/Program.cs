using GarageSpace.EventBus.SDK.Extensions;
using GarageSpace.NotificationService.Services;
using GarageSpace.NotificationService.Services.Interfaces;
using GarageSpace.NotificationService.Templates.Email;
using GarageSpace.NotificationService.Templates.Extensions;
using GarageSpace.NotificationService.Worker;
using GarageSpace.NotificationService.Worker.Consumers;
using Microsoft.AspNetCore.Builder;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection(EmailSettings.SectionName));

builder.Services.AddSingleton<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddEmailTemplates();
builder.Services.AddDatabase(builder.Configuration);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddMassTransitConsumers(builder.Configuration, x =>
{
    x.AddConsumer<SubscriberEventsConsumer>();
});


var host = builder.Build();
host.Run();