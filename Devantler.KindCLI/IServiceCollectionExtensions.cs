using CliWrap;
using Microsoft.Extensions.DependencyInjection;

namespace Devantler.KindCLI;

public static class IServiceCollectionExtensions
{
    public static async Task<IServiceCollection> AddKindCliServiceAsync(this IServiceCollection services)
    {
        return services.AddScoped<IKindCliService, KindCliService>();
    }
}
