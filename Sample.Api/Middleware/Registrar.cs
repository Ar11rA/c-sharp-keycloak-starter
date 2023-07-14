using Microsoft.EntityFrameworkCore;
using Sample.Api.Clients;
using Sample.Api.Clients.Interfaces;
using Sample.Api.Config;
using Sample.Api.Repositories;
using Sample.Api.Repositories.Interfaces;
using Sample.Api.Services;
using Sample.Api.Services.Interfaces;

namespace Sample.Api.Middleware;

public static class Registrar
{
    public static void RegisterWebServices(this IServiceCollection services)
    {
        services
            .AddScoped<IFruitService, FruitService>()
            .AddScoped<IFruitRepository, FruitRepository>()
            .AddScoped<IGeneratorService, GeneratorService>();
        services
            .AddTransient<RetryDelegateHandler>()
            .AddHttpClient<IQuoteClient, QuoteClient>()
            .AddHttpMessageHandler<RetryDelegateHandler>();
        services
            .AddHttpClient<IFactClient, FactClient>();
    }

    public static void RegisterDataServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration["DbContext"] ??
                                  throw new Exception("No DB connection string set!");
        services.AddDbContext<ApplicationDbContext>(
            options => { options.UseNpgsql(connectionString); }
        );
    }
}
