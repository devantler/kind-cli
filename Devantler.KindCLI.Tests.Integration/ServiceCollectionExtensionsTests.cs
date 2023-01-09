using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devantler.KindCLI.Tests.Integration;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddKindCliService_RegistersScopedService()
    {
        //Arrange
        ServiceCollection services = new();
        IConfigurationRoot configuration = new ConfigurationBuilder().Build();

        //Act
        _ = services.AddKindCliService();

        //Assert
        _ = services.Should().ContainSingle(x =>
            x.ServiceType == typeof(IKindCliService) &&
            x.Lifetime == ServiceLifetime.Scoped);
    }
}
