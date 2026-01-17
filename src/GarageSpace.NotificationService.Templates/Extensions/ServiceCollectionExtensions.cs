using GarageSpace.NotificationService.Interfaces;
using GarageSpace.NotificationService.Templates.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GarageSpace.NotificationService.Templates.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailTemplates(this IServiceCollection services)
    {
        services.AddMvcCore()
            .AddRazorViewEngine()
            .AddRazorRuntimeCompilation();

        services.AddScoped<IEmailTemplateRendererService, RazorEmailTemplateService>();
        
        return services;
    }
}
