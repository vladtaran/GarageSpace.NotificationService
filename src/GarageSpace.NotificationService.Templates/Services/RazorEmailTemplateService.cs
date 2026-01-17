using GarageSpace.NotificationService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace GarageSpace.NotificationService.Templates.Services;

public class RazorEmailTemplateService : IEmailTemplateRendererService
{
    private readonly IRazorViewEngine _viewEngine;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<IEmailTemplateRendererService> _logger;

    private static readonly Dictionary<string, string> TemplateSubjects = new()
    {
        { "NewSubscriberEmail", "You have a new subscriber!" }
    };

    public RazorEmailTemplateService(
        IRazorViewEngine viewEngine,
        ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider,
        ILogger<IEmailTemplateRendererService> logger)
    {
        _viewEngine = viewEngine;
        _tempDataProvider = tempDataProvider;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<string> RenderTemplateAsync<T>(string templateName, T model, CancellationToken cancellationToken = default)
    {
        var normalizedName = templateName.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase)
            ? templateName.Substring(0, templateName.Length - 7)
            : templateName;

        var viewPaths = new[]
        {
            normalizedName,
            $"Views/{normalizedName}", 
            $"/Views/{normalizedName}",
            $"Views/{normalizedName}.cshtml",
            $"/Views/{normalizedName}.cshtml"
        };

        var actionContext = GetActionContext();
        IView? view = null;
        string? foundPath = null;

        foreach (var viewPath in viewPaths)
        {
            var getViewResult = _viewEngine.GetView(null, viewPath, true);
            if (getViewResult.Success)
            {
                view = getViewResult.View;
                foundPath = viewPath;
                break;
            }

            var findViewResult = _viewEngine.FindView(actionContext, viewPath, true);
            if (findViewResult.Success)
            {
                view = findViewResult.View;
                foundPath = viewPath;
                break;
            }
        }

        if (view == null)
        {
            var allSearchedLocations = viewPaths.SelectMany(path =>
            {
                var getResult = _viewEngine.GetView(null, path, true);
                var findResult = _viewEngine.FindView(actionContext, path, true);
                return getResult.SearchedLocations.Concat(findResult.SearchedLocations);
            }).Distinct();

            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{templateName}'. The following locations were searched:" }.Concat(allSearchedLocations));

            _logger.LogError(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }

        _logger.LogDebug("Found view '{TemplateName}' at path '{ViewPath}'", templateName, foundPath);

        using var output = new StringWriter();
        var viewContext = new ViewContext(
            actionContext,
            view,
            new ViewDataDictionary<T>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            },
            new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
            output,
            new HtmlHelperOptions());

        await view.RenderAsync(viewContext);
        return output.ToString();
    }

    public string GetSubject(string templateName)
    {
        var normalizedName = templateName.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase)
            ? templateName.Substring(0, templateName.Length - 7)
            : templateName;

        return TemplateSubjects.TryGetValue(normalizedName, out var subject) 
            ? subject 
            : "Notification";
    }

    private ActionContext GetActionContext()
    {
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProvider
        };
        return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
    }
}
