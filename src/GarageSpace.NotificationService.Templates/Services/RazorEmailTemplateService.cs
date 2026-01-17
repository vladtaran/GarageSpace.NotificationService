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
        { "NewFollowerEmail", "You have a new follower!" }
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
        var viewPath = $"Templates/{templateName}";
        
        var actionContext = GetActionContext();
        var view = FindView(actionContext, viewPath);

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
        return TemplateSubjects.TryGetValue(templateName, out var subject) 
            ? subject 
            : "Notification";
    }

    private IView FindView(ActionContext actionContext, string viewName)
    {
        var getViewResult = _viewEngine.GetView(null, viewName, true);
        if (getViewResult.Success)
        {
            return getViewResult.View;
        }

        var findViewResult = _viewEngine.FindView(actionContext, viewName, true);
        if (findViewResult.Success)
        {
            return findViewResult.View;
        }

        var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
        var errorMessage = string.Join(
            Environment.NewLine,
            new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));

        _logger.LogError(errorMessage);
        throw new InvalidOperationException(errorMessage);
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
