namespace Sample.Api.Middleware;

public class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpLoggingMiddleware> _logger;

    public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        HttpResponse response = context.Response;
        HttpRequest request = context.Request;
        response.OnCompleted(() =>
        {
            _logger.LogInformation($"Received {response.StatusCode} for {request.Method} {request.Path}");
            return Task.CompletedTask;
        });
        await _next(context);
    }
}
