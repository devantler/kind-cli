using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devantler.KindCLI.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddKindCliService_RegistersScopedService()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        services.AddKindCliService();

        var _ = Assert.Single(services, x =>
            x.ServiceType == typeof(IKindCliService) &&
            x.Lifetime == ServiceLifetime.Scoped);
    }
}
