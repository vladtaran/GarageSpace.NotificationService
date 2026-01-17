using GarageSpace.NotificationService.Interfaces;
using GarageSpace.NotificationService.Templates.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

        return services;
    }
}