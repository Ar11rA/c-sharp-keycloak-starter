using MediatR;
using Sample.Api.Repositories.Interfaces;

namespace Sample.Api.Handlers.Fruit;

public class GetFruitHandler : IRequestHandler<GetFruitQuery, List<Models.Fruit>>
{
    private readonly IFruitRepository _fruitRepository;

    public GetFruitHandler(IFruitRepository fruitRepository)
    {
        _fruitRepository = fruitRepository;
    }

    public async Task<List<Models.Fruit>> Handle(GetFruitQuery request, CancellationToken cancellationToken)
    {
        return await _fruitRepository.GetFruits();
    }
}
