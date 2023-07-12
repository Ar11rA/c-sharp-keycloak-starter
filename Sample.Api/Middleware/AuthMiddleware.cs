using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Sample.Api.Config;

namespace Sample.Api.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;

    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request is annotated with the [Authorize] attribute
        IEndpointFeature? feature = context.Features.Get<IEndpointFeature>();
        if (feature == null)
        {
            await _next.Invoke(context);
            return;
        }

        Endpoint? endpoint = feature.Endpoint;
        if (endpoint == null)
        {
            await _next.Invoke(context);
            return;
        }

        bool isAuthRequired = endpoint.Metadata.Any(f => f is AuthAttribute);
        if (isAuthRequired)
        {
            AuthAttribute? attribute = feature.Endpoint!.Metadata
                .GetMetadata<AuthAttribute>();
            _logger.LogInformation("Authentication required for route");
            string[]? groups = attribute!.Groups;
            if (groups != null)
            {
                _logger.LogInformation(groups.Length.ToString());
            }
        }

        await _next.Invoke(context);
    }
}
