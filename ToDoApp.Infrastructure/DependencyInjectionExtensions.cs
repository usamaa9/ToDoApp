using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoApp.Core.Repositories;
using ToDoApp.Infrastructure.Repositories;
using ToDoApp.Infrastructure.Repositories.Mongo;

namespace ToDoApp.Infrastructure;

/// <summary>
/// Extension methods for setting up infrastructure services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds the infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, ILogger logger)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDatabase"));

        var sp = services.BuildServiceProvider();

        var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

        if (!settings.Enabled)
        {
            logger.LogWarning("Using in-memory repository.");
            services.AddSingleton<IToDoRepository, InMemoryToDoRepository>();
        }
        else
        {
            logger.LogInformation("Using MongoDB repository.");
            services.AddScoped<IToDoRepository, MongoToDoRepository>();

            services.AddScoped(serviceProvider =>
            {
                var client = new MongoClient(settings.ConnectionString);
                return client.GetDatabase(settings.DatabaseName);
            });
        }

        return services;
    }
}
