using Sample.Api.DTO;

namespace Sample.Api.Clients.Interfaces;

public interface IAuthClient
{
    public Task<string> GetRedirectUrl();
    public Task<TokenDetails> Login(LoginRequest loginRequest);
    public Task<TokenDetails> Refresh(TokenRequest tokenRequest);
    public Task<TokenValidationResponse> Introspect(TokenRequest tokenRequest);
}
