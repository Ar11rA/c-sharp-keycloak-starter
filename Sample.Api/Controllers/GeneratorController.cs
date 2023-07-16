using Microsoft.AspNetCore.Mvc;
using Sample.Api.DTO;
using Sample.Api.Services.Interfaces;

namespace Sample.Api.controllers;

[ApiController]
[Route("api/generator")]
public class GeneratorController
{
    private readonly IGeneratorService _generatorService;

    public GeneratorController(IGeneratorService generatorService)
    {
        _generatorService = generatorService;
    }

    [HttpGet("quote")]
    public async Task<QuoteResponse> GetQuote()
    {
        return await _generatorService.GetQuote();
    }

    [HttpGet("fact")]
    public async Task<string> GetFact()
    {
        return await _generatorService.GetFact();
    }
}
