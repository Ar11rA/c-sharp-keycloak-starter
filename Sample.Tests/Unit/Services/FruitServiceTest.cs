using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Sample.Api.Config;
using Sample.Api.Models;
using Sample.Api.Repositories.Interfaces;
using Sample.Api.Services;
using Sample.Api.Services.Interfaces;

namespace Sample.Tests.Unit.Services;

public class FruitServiceTest
{
    private readonly Mock<IFruitRepository> _fruitRepository;
    private readonly IFruitService _fruitService;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<FruitService>> _logger;

    public FruitServiceTest()
    {
        _fruitRepository = new Mock<IFruitRepository>();
        _logger = new Mock<ILogger<FruitService>>();
        _mapper = new MapperConfiguration(c =>
            c.AddProfile<MappingProfile>()).CreateMapper();
        _fruitService = new FruitService(
            _fruitRepository.Object,
            _mapper,
            _logger.Object);
    }

    [Fact]
    public async Task TestGetFruits_NullName()
    {
        _fruitRepository.Setup(f => f.GetFruits()).ReturnsAsync(new List<Fruit>
        {
            new() {Id = 1},
            new() {Id = 2}
        });
        List<Fruit> fruits = await _fruitService.GetFruits(null);
        Assert.Equal(2, fruits.Count);
        Assert.Equal(1, fruits[0].Id);
        Assert.Equal(2, fruits[1].Id);
    }
}