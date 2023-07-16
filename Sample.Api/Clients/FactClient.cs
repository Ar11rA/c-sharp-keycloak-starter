using Sample.Api.Clients.Interfaces;
using Sample.Api.Exceptions;

namespace Sample.Api.Clients;

public class FactClient : IFactClient
{
    private readonly HttpClient _httpClient;

    public FactClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetFact()
    {
        HttpResponseMessage result = await _httpClient.GetAsync(
            new Uri("https://uselessfacts.jsph.pl/api/v2/facts/random"));
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpException(503, "Service Unavailable");
        }

        string response = await result.Content.ReadAsStringAsync();
        return response;
    }
}
