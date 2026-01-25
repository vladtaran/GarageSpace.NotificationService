using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using GarageSpace.NotificationService.Templates.Extensions;
using GarageSpace.NotificationService.IntegrationTests.Infrastructure;
using GarageSpace.NotificationService.Templates.Email;
using GarageSpace.NotificationService.Services.Interfaces;

namespace GarageSpace.NotificationService.IntegrationTests.Email;

public sealed class EmailTestFixture : IDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public IEmailService EmailService { get; }
    public IEmailTemplateRendererService EmailTemplateRendererService { get; }
    public INotificationService NotificationService { get; }
    public IConfiguration Configuration { get; }

    public EmailTestFixture()
    {
        var basePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

        Configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.Test.json", optional: false)
            .AddEnvironmentVariables()
            .AddUserSecrets<EmailTestFixture>()
            .Build();

        var services = new ServiceCollection();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        services.AddSingleton<IWebHostEnvironment>(new TestWebHostEnvironment
        {
            ApplicationName = "GarageSpace.NotificationService.IntegrationTests",
            EnvironmentName = Environments.Development,
            ContentRootPath = basePath,
            ContentRootFileProvider = new PhysicalFileProvider(basePath)
        });

        services.Configure<EmailSettings>(
            Configuration.GetSection(EmailSettings.SectionName));

        services.AddSingleton<IEmailService, SmtpEmailService>();
        services.AddScoped<INotificationService, Services.NotificationService>();

        // Razor templates
        services.AddEmailTemplates();

        services.AddSingleton(Configuration);

        _serviceProvider = services.BuildServiceProvider();

        EmailService = _serviceProvider.GetRequiredService<IEmailService>();
        EmailTemplateRendererService = _serviceProvider.GetRequiredService<IEmailTemplateRendererService>();
        NotificationService = _serviceProvider.GetRequiredService<INotificationService>();
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
    }
}

