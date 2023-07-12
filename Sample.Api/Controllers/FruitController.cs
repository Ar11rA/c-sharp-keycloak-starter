using Microsoft.AspNetCore.Mvc;
using Sample.Api.Config;
using Sample.Api.DTO;
using Sample.Api.Models;
using Sample.Api.Services.Interfaces;

namespace Sample.Api.controllers;

[ApiController]
[Route("/api/[controller]")]
public class FruitController
{
    private readonly IFruitService _fruitService;

    public FruitController(IFruitService fruitService)
    {
        _fruitService = fruitService;
    }

    [Auth]
    [HttpGet]
    public async Task<List<Fruit>> Get(string? name)
    {
        return await _fruitService.GetFruits(name);
    }

    [Auth("Member", "Leader")]
    [HttpPost]
    public async Task<Fruit> Create(FruitRequest fruitRequest)
    {
        return await _fruitService.CreateFruit(fruitRequest);
    }
}
