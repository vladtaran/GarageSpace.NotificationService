namespace GarageSpace.NotificationService.Services.Interfaces;

public interface IEmailTemplateRendererService
{
    Task<string> RenderTemplateAsync<T>(string templateName, T model, CancellationToken cancellationToken = default);
}