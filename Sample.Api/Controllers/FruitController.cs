using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Config;
using Sample.Api.DTO;
using Sample.Api.Handlers.Fruit;
using Sample.Api.Models;
using Sample.Api.Services.Interfaces;

namespace Sample.Api.controllers;

[ApiController]
[Route("/api/fruits")]
public class FruitController
{
    private readonly IFruitService _fruitService;
    private readonly ISender _mediator;

    public FruitController(IFruitService fruitService, ISender mediator)
    {
        _fruitService = fruitService;
        _mediator = mediator;
    }

    [Auth]
    [HttpGet]
    public async Task<List<Fruit>> Get(string? name)
    {
        return await _fruitService.GetFruits(name);
    }

    [HttpGet("group")]
    public async Task<Dictionary<string, List<Fruit>>> GroupByName()
    {
        return await _fruitService.GroupFruits();
    }

    [Auth("Member", "Leader")]
    [HttpPost]
    public async Task<Fruit> Create(FruitRequest fruitRequest)
    {
        return await _fruitService.CreateFruit(fruitRequest);
    }

    [HttpGet("mediator")]
    public async Task<List<Fruit>> GetFromMediatR(string name)
    {
        return await _mediator.Send(new GetFruitQuery(name));
    }

    [HttpPost("mediator")]
    public async Task<Fruit> CreateFromMediatR(FruitRequest fruitRequest)
    {
        return await _mediator.Send(new CreateFruitCommand(fruitRequest));
    }
}
