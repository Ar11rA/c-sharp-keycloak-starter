using Sample.Api.DTO;
using Sample.Api.Models;

namespace Sample.Api.Services.Interfaces;

public interface IFruitService
{
    Task<List<Fruit>> GetFruits(string? name);
    Task<Fruit> CreateFruit(FruitRequest fruitRequest);
}
