using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GarageSpace.NotificationService.Services;
using GarageSpace.NotificationService.Interfaces;

namespace GarageSpace.NotificationService.IntegrationTests;

public class EmailTestFixture : IDisposable
{
    public IEmailService EmailService { get; }
    public INotificationService NotificationService { get; }
    public IConfiguration Configuration { get; }

    public EmailTestFixture()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets<EmailTestFixture>();

        Configuration = configurationBuilder.Build();

        var services = new ServiceCollection();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        services.Configure<EmailSettings>(
            Configuration.GetSection(EmailSettings.SectionName));

        services.AddSingleton<IEmailService, SmtpEmailService>();
        services.AddScoped<INotificationService, Services.NotificationService>();

        services.AddSingleton(Configuration);

        var serviceProvider = services.BuildServiceProvider();

        EmailService = serviceProvider.GetRequiredService<IEmailService>();
        NotificationService = serviceProvider.GetRequiredService<INotificationService>();
    }

    public void Dispose() {}
}
