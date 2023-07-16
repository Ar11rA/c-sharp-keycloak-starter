using AutoMapper;
using MediatR;
using Sample.Api.DTO;
using Sample.Api.Repositories.Interfaces;

namespace Sample.Api.Handlers.Fruit;

public sealed record CreateFruitCommand(FruitRequest Request) : IRequest<Models.Fruit>;

public class CreateFruitHandler : IRequestHandler<CreateFruitCommand, Models.Fruit>
{
    private readonly IFruitRepository _fruitRepository;
    private readonly IMapper _mapper;

    public CreateFruitHandler(IFruitRepository fruitRepository, IMapper mapper)
    {
        _fruitRepository = fruitRepository;
        _mapper = mapper;
    }

    public Task<Models.Fruit> Handle(CreateFruitCommand request, CancellationToken cancellationToken)
    {
        Models.Fruit fruit = _mapper.Map<Models.Fruit>(request.Request);
        return _fruitRepository.CreateFruit(fruit);
    }
}
