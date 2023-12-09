using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using toDoApp.Api.Attributes;

namespace toDoApp.Api.Middlewares;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;

        if (endpoint != null)
        {
            var hasTokenRequiredAttribute = endpoint.Metadata.GetMetadata<TokenRequiredAttribute>();
            if (hasTokenRequiredAttribute != null)
            {
                var token = context.Request.Headers["Authorization"];
                if (string.IsNullOrWhiteSpace(token) || token != "123")
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Token is required or invalid for this endpoint.", Encoding.UTF8);
                    return;
                }
            }
        }

        await _next(context);
    }
}