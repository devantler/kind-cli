using Microsoft.Extensions.DependencyInjection;

namespace Devantler.KindCLI;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddKindCliService(this IServiceCollection services)
    {
        return services.AddScoped<IKindCliService, KindCliService>();
    }
}
