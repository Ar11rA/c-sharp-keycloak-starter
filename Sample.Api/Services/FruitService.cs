using AutoMapper;
using Sample.Api.DTO;
using Sample.Api.Exceptions;
using Sample.Api.Models;
using Sample.Api.Repositories.Interfaces;
using Sample.Api.Services.Interfaces;

namespace Sample.Api.Services;

public class FruitService : IFruitService
{
    private readonly IFruitRepository _fruitRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FruitService> _logger;

    public FruitService(IFruitRepository fruitRepository, IMapper mapper, ILogger<FruitService> logger)
    {
        _fruitRepository = fruitRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<Fruit>> GetFruits(string? name)
    {
        _logger.LogInformation(name);
        if (name != null)
        {
            _logger.LogInformation("Returning fruits with name search");
            List<Fruit> fruits = await _fruitRepository.GetFruitsByName(name);
            if (fruits.Count == 0)
            {
                throw new HttpException(404, "Fruits not found!");
            }

            return fruits;
        }

        _logger.LogInformation("Returning fruits without name search");
        return await _fruitRepository.GetFruits();
    }

    public async Task<Fruit> CreateFruit(FruitRequest fruitRequest)
    {
        Fruit fruit = _mapper.Map<Fruit>(fruitRequest);
        return await _fruitRepository.CreateFruit(fruit);
    }
}
