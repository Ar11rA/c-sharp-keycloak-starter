using System.Text.Json;
using Sample.Api.Clients.Interfaces;
using Sample.Api.DTO;
using Sample.Api.Exceptions;

namespace Sample.Api.Clients;

public class QuoteClient : IQuoteClient
{
    private readonly HttpClient _httpClient;


    public QuoteClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<QuoteResponse> GetQuote()
    {
        HttpResponseMessage result = await _httpClient.GetAsync(
            new Uri("http://localhost:3001/quote"));
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpException(503, "Service Unavailable");
        }

        string response = await result.Content.ReadAsStringAsync();
        QuoteResponse? quoteResponse = JsonSerializer.Deserialize<QuoteResponse>(response);
        if (quoteResponse == null)
        {
            throw new HttpException(500, "Json conversion failed");
        }

        return quoteResponse;
    }
}
