using CliWrap;
using Microsoft.Extensions.DependencyInjection;

namespace Devantler.KindCLI;

public static class IServiceCollectionExtensions
{
    public static async Task<IServiceCollection> AddKindCliServiceAsync(this IServiceCollection services)
    {
        await Cli.Wrap("chmod").WithArguments("+x assets/kind-*").ExecuteAsync();
        return services.AddScoped<IKindCliService, KindCliService>();
    }
}
