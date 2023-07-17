using Microsoft.AspNetCore.Mvc;
using Sample.Api.Clients.Interfaces;
using Sample.Api.DTO;

namespace Sample.Api.controllers;

[ApiController]
[Route("api/auth")]
public class AuthController
{
    private readonly IAuthClient _authClient;

    public AuthController(IAuthClient authClient)
    {
        _authClient = authClient;
    }

    [HttpGet("redirect")]
    public async Task<string> GetRedirectUrl()
    {
        return await _authClient.GetRedirectUrl();
    }

    [HttpPost("login")]
    public async Task<TokenDetails> Login(LoginRequest loginRequest)
    {
        return await _authClient.Login(loginRequest);
    }

    [HttpPost("refresh")]
    public async Task<TokenDetails> Refresh(TokenRequest tokenRequest)
    {
        return await _authClient.Refresh(tokenRequest);
    }
}
