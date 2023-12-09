using Microsoft.AspNetCore.Mvc.Filters;
using toDoApp.Api.Models.Log;

namespace toDoApp.Api.MvcFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ResponseLoggingActionAttribute : Attribute, IResourceFilter, IAsyncResourceFilter
{
    private readonly ILogger<ResponseLoggingActionAttribute> _logger;

    public ResponseLoggingActionAttribute(ILogger<ResponseLoggingActionAttribute> logger)
    {
        _logger = logger;
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        LogResponse(context);
    }

    private void LogResponse(ResourceExecutedContext context)
    {
        var loggingModel = new CustomLoggingModel
        {
            CorrelationId = GetCorrelationId(context),
            StatusCode = context.HttpContext.Response.StatusCode,
            HttpMethod = context.HttpContext.Request.Method,
            Path = context.HttpContext.Request.Path,
            ResponseBody = GetResponseBody(context)
        };

        var logMessage = "Custom Response Log: " +
                         string.Join(", ",
                             loggingModel.GetType().GetProperties().Select(property =>
                                 $"{property.Name}={property.GetValue(loggingModel)}"));

        _logger.LogInformation(logMessage);
    }

    private string GetCorrelationId(ResourceExecutedContext context)
    {
        return context.HttpContext.Items["CorrelationId"]?.ToString()!;
    }

    private string? GetResponseBody(ResourceExecutedContext context)
    {
        try
        {
            var result = context.Result as Microsoft.AspNetCore.Mvc.ObjectResult;
            return result?.Value?.ToString()!;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var resultContext = await next();

        LogResponse(resultContext);
    }
}