using System.Net;
using Xunit.Abstractions;
using Sample.Api.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Sample.Tests.Utils.Clients;

namespace Sample.Tests.Integration
{
    public class FruitTest
    {
        //If required to debug further for any of the failed test cases.
        private readonly ITestOutputHelper _output;

        private readonly FruitsClient _client;

        public FruitTest(ITestOutputHelper output)
        {
            _output = output;
            _client = new FruitsClient(new WebApplicationFactory<Program>().CreateClient());
        }

        [Fact]
        public async Task GetFruitByName_NullName_Success()
        {
            var response = await _client.GetFruitsAsync(null);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task GetFruitByName_NameAsApple_Success()
        {
            // creating fruit before get request
            FruitRequest fruitRequest = new FruitRequest()
            {
                Name = "Apple",
                Description = "Washington Apples",
                Class = "Medium"
            };
            await _client.CreateFruitAsync(fruitRequest);
            
            var response = await _client.GetFruitsAsync("Apple");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetFruitByName_NameAsBanana_Failure()
        {
            var response = await _client.GetFruitsAsync("Banana");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateFruit_Success()
        {
            FruitRequest fruitRequest = new FruitRequest()
            {
                Name = "Apple",
                Description = "Washington Apples",
                Class = "Medium"
            };
            var response = await _client.CreateFruitAsync(fruitRequest);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateFruit_Failure()
        {
            FruitRequest fruitRequest = new FruitRequest()
            {
                Name = "Orange",
                Class = "Medium"
            };
            var response = await _client.CreateFruitAsync(fruitRequest);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

