using Sample.Api.DTO;

namespace Sample.Api.Services.Interfaces;

public interface IGeneratorService
{
    public Task<QuoteResponse> GetQuote();
    public Task<string> GetFact();
}
