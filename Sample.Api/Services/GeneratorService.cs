using Sample.Api.Clients.Interfaces;
using Sample.Api.DTO;
using Sample.Api.Services.Interfaces;

namespace Sample.Api.Services;

public class GeneratorService : IGeneratorService
{
    private readonly IQuoteClient _quoteClient;
    private readonly IFactClient _factClient;

    public GeneratorService(IQuoteClient quoteClient, IFactClient factClient)
    {
        _quoteClient = quoteClient;
        _factClient = factClient;
    }

    public async Task<QuoteResponse> GetQuote()
    {
        return await _quoteClient.GetQuote();
    }

    public async Task<string> GetFact()
    {
        return await _factClient.GetFact();
    }
}
