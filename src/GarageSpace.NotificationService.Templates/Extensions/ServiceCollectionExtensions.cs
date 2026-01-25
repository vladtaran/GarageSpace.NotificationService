using GarageSpace.NotificationService.Services.Interfaces;
using GarageSpace.NotificationService.Templates.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace GarageSpace.NotificationService.Templates.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailTemplates(this IServiceCollection services)
    {
        services.TryAddSingleton<DiagnosticListener>(new DiagnosticListener("RazorTemplates"));
        services.TryAddSingleton<DiagnosticSource>(sp => sp.GetRequiredService<DiagnosticListener>());

        var templatesAssembly = typeof(TemplatesAssemblyMarker).Assembly;

        services
            .AddMvcCore()
            .AddRazorViewEngine()
            .AddApplicationPart(templatesAssembly);

        services.AddScoped<IEmailTemplateRendererService, RazorEmailTemplateService>();
        services.AddScoped<IEmailNotificationSender, EmailNotificationSender>();
        services.AddScoped<IEmailService, SmtpEmailService>();

        var basePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

        services.AddSingleton<IWebHostEnvironment>(new WebHostEnvironment
        {
            ApplicationName = "GarageSpace.NotificationService.Worker",
            EnvironmentName = Environments.Development,
            ContentRootPath = basePath,
            ContentRootFileProvider = new PhysicalFileProvider(basePath)
        });

        return services;
    }
}