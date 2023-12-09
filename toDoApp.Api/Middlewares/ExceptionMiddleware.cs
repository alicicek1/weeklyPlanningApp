using System.Net;
using Newtonsoft.Json;

namespace toDoApp.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = (int) HttpStatusCode.InternalServerError;
        var message = "Internal Server Error";

        if (exception is UnauthorizedAccessException)
        {
            statusCode = (int) HttpStatusCode.Unauthorized;
            message = "Unauthorized Access";
        }

        var response = new {StatusCode = statusCode, Message = message};

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}