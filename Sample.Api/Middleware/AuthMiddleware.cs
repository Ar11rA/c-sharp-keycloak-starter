using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Sample.Api.Clients.Interfaces;
using Sample.Api.Config;
using Sample.Api.DTO;

namespace Sample.Api.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;
    private readonly IAuthClient _authClient;

    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger, IAuthClient authClient)
    {
        _next = next;
        _logger = logger;
        _authClient = authClient;
    }

    private async Task<(bool, bool)> ValidateBearerAuth(HttpContext context, string[]? groups)
    {
        IHeaderDictionary headers = context.Request.Headers;
        StringValues auth = headers.Authorization;
        if (auth.IsNullOrEmpty())
        {
            _logger.LogError("Authentication Failed: No auth header");
            return (false, false);
        }

        string authStr = auth.ToString();
        if (!authStr.StartsWith("Bearer"))
        {
            _logger.LogError("Authentication Failed: Unsupported auth scheme");
            context.Response.StatusCode = 401;
            return (false, false);
        }

        string token = authStr.Split(" ")[1];
        TokenRequest tokenRequest = new() {Token = token};
        TokenValidationResponse tokenValidationResponse = await _authClient.Introspect(tokenRequest);
        if (!tokenValidationResponse.IsActive)
        {
            _logger.LogError("Authentication Failed: Token expired");
            context.Response.StatusCode = 401;
            return (false, false);
        }

        HashSet<string> assignedGroups = tokenValidationResponse.Groups.ToHashSet();
        bool isAuthorized = groups?.Any(group => assignedGroups.Contains(group)) ?? tokenValidationResponse.IsActive;
        if (!isAuthorized)
        {
            _logger.LogError("Authentication Failed: Role not matched");
            return (true, false);
        }
        return (true, true);
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
        if (!isAuthRequired)
        {
            await _next.Invoke(context);
            return;
        }

        AuthAttribute? attribute = feature.Endpoint!.Metadata
            .GetMetadata<AuthAttribute>();
        _logger.LogInformation("Authentication required for route");
        string[]? groups = attribute!.Groups;
        (bool isAuthenticated, bool isAuthorized) = await ValidateBearerAuth(context, groups);
        if (!isAuthenticated)
        {
            context.Response.StatusCode = 401;
            return;
        }
        
        if (!isAuthorized)
        {
            context.Response.StatusCode = 403;
            return;
        }
        
        _logger.LogInformation("Keycloak authentication passed!");

        await _next.Invoke(context);
    }
}
