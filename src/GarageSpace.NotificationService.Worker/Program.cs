using GarageSpace.NotificationService.Services;
using GarageSpace.NotificationService.Services.Interfaces;
using GarageSpace.NotificationService.Templates.Email;
using GarageSpace.NotificationService.Templates.Extensions;
using GarageSpace.NotificationService.Worker;
using GarageSpace.NotificationService.Worker.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Builder;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection(EmailSettings.SectionName));

builder.Services.AddSingleton<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddEmailTemplates();
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SubscriberEventsConsumer>();

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