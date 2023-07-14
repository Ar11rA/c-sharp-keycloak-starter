namespace Sample.Api.Config;

public class RetryDelegateHandler : DelegatingHandler
{
    private const int MaxRetries = 3;
    private readonly ILogger<RetryDelegateHandler> _logger; 
    private int _backOff;

    public RetryDelegateHandler(ILogger<RetryDelegateHandler> logger, IConfiguration configuration)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _backOff = 500;
        HttpResponseMessage response = new();
        for (int i = 0; i < MaxRetries; i++)
        {
            _logger.LogInformation("here with retry: " + i);
            response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode) {
                return response;
            }
            _logger.LogInformation("Delay of: " + _backOff);
            await Task.Delay(_backOff);
            _backOff *= 2;
        }

        return response;
    }
}
