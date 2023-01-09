using Microsoft.Extensions.DependencyInjection;

namespace Devantler.KindCLI;

/// <summary>
/// An interface for extension methods to add the Kind CLI service to the DI container.
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Kind CLI service to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddKindCliService(this IServiceCollection services) =>
        services.AddScoped<IKindCliService, KindCliService>();
}
