using System.Text;
using System.Text.Json;
using Sample.Api.DTO;
using Sample.Api.Exceptions;

namespace Sample.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    private ReadOnlyMemory<byte> ErrorToStr(ErrorResponse response)
    {
        string res = JsonSerializer.Serialize(response);
        return new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(res));
    }

    public async Task Invoke(HttpContext context)
    {
        // Try to process the request
        try
        {
            await _next(context);
        }
        // Catch any exceptions
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception in processing request");
            context.Response.ContentType = "application/json";
            ErrorResponse response;
            switch (exception)
            {
                case HttpException httpException:
                    context.Response.StatusCode = httpException.Code;
                    response = new ErrorResponse
                    {
                        Code = httpException.Code,
                        Message = httpException.Message
                    };
                    await context.Response.Body.WriteAsync(ErrorToStr(response));
                    break;
                default:
                    response = new ErrorResponse
                    {
                        Code = 500,
                        Message = "Internal Server error"
                    };
                    context.Response.StatusCode = response.Code;
                    await context.Response.Body.WriteAsync(ErrorToStr(response));
                    break;
            }
        }
    }
}
