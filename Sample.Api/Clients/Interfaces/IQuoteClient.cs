using Sample.Api.DTO;

namespace Sample.Api.Clients.Interfaces;

public interface IQuoteClient
{
    public Task<QuoteResponse> GetQuote();
}
