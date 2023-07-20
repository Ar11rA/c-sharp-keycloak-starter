using Microsoft.AspNetCore.WebUtilities;
using Sample.Api.DTO;
using Sample.Api.Middleware;
using Sample.Api.Models;

namespace Sample.Api.Clients
{
    public class FruitsClient : BaseClient
    {

        public FruitsClient(HttpClient client) : base(client)
        {
        }

        public Task<ClientResponse<PagedView<Fruit>>> GetFruitsAsync(string? fruitName)
        {
            var parameters = new Dictionary<string, string?>();
            parameters.Add("name", fruitName);
            var queryPath = QueryHelpers.AddQueryString(Routes.Fruits, parameters);
            return GetAsync<PagedView<Fruit>>(queryPath);
        }

        public Task<ClientResponse<Fruit>> CreateFruitAsync(FruitRequest fruitRequest)
        {
            return PostAsync<FruitRequest, Fruit>(Routes.Fruits, fruitRequest);
        }
    }
}

