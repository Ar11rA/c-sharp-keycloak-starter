using System.Text.Json;
using Sample.Api.Clients.Interfaces;
using Sample.Api.DTO;
using Sample.Api.Exceptions;

namespace Sample.Api.Clients;

public class AuthClient : IAuthClient
{
    private readonly ILogger<AuthClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _realm;

    public AuthClient(ILogger<AuthClient> logger, HttpClient httpClient, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = httpClient;
        _clientId = configuration["KeycloakClientId"]!;
        _clientSecret = configuration["KeycloakClientSecret"]!;
        _realm = configuration["KeycloakRealm"]!;

        httpClient.BaseAddress = new Uri(configuration["KeycloakClientUrl"]!);
    }

    public async Task<string> GetRedirectUrl()
    {
        _logger.LogInformation("Forming redirect url");
        string baseAddress = $"{_httpClient.BaseAddress}/realms/{_realm}/protocol/openid-connect/auth";
        return
            $"{baseAddress}?client_id={_clientId}&client_secret={_clientSecret}&response_type=code";
    }

    public async Task<TokenDetails> Login(LoginRequest loginRequest)
    {
        Dictionary<string, string> body = new()
        {
            {"code", loginRequest.Code},
            {"client_id", _clientId},
            {"client_secret", _clientSecret},
            {"grant_type", "authorization_code"}
        };

        string tokenUri = $"{_httpClient.BaseAddress}/realms/{_realm}/protocol/openid-connect/token";
        HttpResponseMessage result = await _httpClient.PostAsync(new Uri(tokenUri), new FormUrlEncodedContent(body));
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpException(400, "Bad code");
        }

        string response = await result.Content.ReadAsStringAsync();
        TokenDetails tokenDetails = JsonSerializer.Deserialize<TokenDetails>(response)!;
        return tokenDetails;
    }

    public async Task<TokenDetails> Refresh(TokenRequest tokenRequest)
    {
        Dictionary<string, string> body = new()
        {
            {"refresh_token", tokenRequest.Token},
            {"client_id", _clientId},
            {"client_secret", _clientSecret},
            {"grant_type", "refresh_token"}
        };

        string tokenUri = $"{_httpClient.BaseAddress}/realms/{_realm}/protocol/openid-connect/token";
        HttpResponseMessage result = await _httpClient.PostAsync(new Uri(tokenUri), new FormUrlEncodedContent(body));
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpException(400, "Invalid refresh token");
        }

        string response = await result.Content.ReadAsStringAsync();
        TokenDetails tokenDetails = JsonSerializer.Deserialize<TokenDetails>(response)!;
        return tokenDetails;
    }

    public async Task<TokenValidationResponse> Introspect(TokenRequest tokenRequest)
    {
        Dictionary<string, string> body = new()
        {
            {"token", tokenRequest.Token},
            {"client_id", _clientId},
            {"client_secret", _clientSecret}
        };

        string tokenUri = $"{_httpClient.BaseAddress}/realms/{_realm}/protocol/openid-connect/token/introspect";
        HttpResponseMessage result = await _httpClient.PostAsync(new Uri(tokenUri), new FormUrlEncodedContent(body));
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpException(400, "Invalid access token");
        }

        string response = await result.Content.ReadAsStringAsync();
        TokenValidationResponse tokenValidationResponse =
            JsonSerializer.Deserialize<TokenValidationResponse>(response)!;
        return tokenValidationResponse;
    }
}
