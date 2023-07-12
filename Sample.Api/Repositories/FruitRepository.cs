using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sample.Api.Config;
using Sample.Api.Models;
using Sample.Api.Repositories.Interfaces;

namespace Sample.Api.Repositories;

public class FruitRepository : IFruitRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public FruitRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Fruit>> GetFruits()
    {
        List<Fruit> fruits = await _applicationDbContext.Fruits.ToListAsync();
        return fruits;
    }

    public async Task<List<Fruit>> GetFruitsByName(string name)
    {
        List<Fruit> fruits = await _applicationDbContext.Fruits
            .Where(fruit => fruit.Name.Contains(name))
            .ToListAsync();
        return fruits;
    }

    public async Task<Fruit> CreateFruit(Fruit fruit)
    {
        EntityEntry<Fruit> created = await _applicationDbContext.Fruits.AddAsync(fruit);
        await _applicationDbContext.SaveChangesAsync();
        return created.Entity;
    }
}
