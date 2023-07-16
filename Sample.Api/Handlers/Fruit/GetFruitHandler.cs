using MediatR;
using Sample.Api.Repositories.Interfaces;

namespace Sample.Api.Handlers.Fruit;

public sealed record GetFruitQuery(string Name) : IRequest<List<Models.Fruit>>;

public class GetFruitHandler : IRequestHandler<GetFruitQuery, List<Models.Fruit>>
{
    private readonly IFruitRepository _fruitRepository;

    public GetFruitHandler(IFruitRepository fruitRepository)
    {
        _fruitRepository = fruitRepository;
    }

    public async Task<List<Models.Fruit>> Handle(GetFruitQuery request, CancellationToken cancellationToken)
    {
        // same logic as our service in normal case
        string name = request.Name;
        return await _fruitRepository.GetFruitsByName(name);
    }
}
