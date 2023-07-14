using MediatR;

namespace Sample.Api.Handlers.Fruit;

public class GetFruitQuery: IRequest<List<Models.Fruit>> {}
