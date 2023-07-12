using Sample.Api.Models;

namespace Sample.Api.Repositories.Interfaces;

public interface IFruitRepository
{
    Task<List<Fruit>> GetFruits();
    Task<List<Fruit>> GetFruitsByName(string name);
    Task<Fruit> CreateFruit(Fruit fruit);
}
