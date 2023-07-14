namespace Sample.Api.Clients.Interfaces;

public interface IFactClient
{
    public Task<string> GetFact();
}
