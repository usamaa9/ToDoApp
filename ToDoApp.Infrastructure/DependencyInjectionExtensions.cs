using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Core.Repositories;
using ToDoApp.Infrastructure.Repositories;

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
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IToDoRepository, ToDoRepository>();

        return services;
    }
}
