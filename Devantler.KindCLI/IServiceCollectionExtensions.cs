using Microsoft.Extensions.DependencyInjection;

namespace Devantler.KindCLI;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddKindCliService(this IServiceCollection services) =>
        services.AddScoped<IKindCliService, KindCliService>();
}
