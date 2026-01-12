using MassTransit;
using TaranSoft.MyGarage.NotificationService;
using TaranSoft.MyGarage.NotificationService.Consumers;
using TaranSoft.MyGarage.NotificationService.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

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
