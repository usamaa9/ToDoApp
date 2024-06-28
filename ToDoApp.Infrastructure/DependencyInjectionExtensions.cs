using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ToDoApp.Core.Repositories;
using ToDoApp.Infrastructure.InMemory;
using ToDoApp.Infrastructure.Mongo;
using ToDoApp.Infrastructure.Mongo.Repositories;

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
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDatabase"));

        var sp = services.BuildServiceProvider();

        var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;

        var logger = sp.GetRequiredService<ILogger<MongoDbSettings>>();

        if (settings.Enabled)
        {
            logger.LogInformation("Using MongoDB for persistence.");

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.Binary));

            services.AddSingleton(new MongoClient(settings.ConnectionString));
            services.AddScoped<IToDoRepository, MongoToDoRepository>();
        }
        else
        {
            logger.LogWarning("Using in-memory for persistence.");
            services.AddSingleton<IToDoRepository, InMemoryToDoRepository>();
        }

        return services;
    }
}
