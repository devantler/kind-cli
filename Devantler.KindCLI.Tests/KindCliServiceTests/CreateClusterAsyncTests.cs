namespace Devantler.KindCLI.Tests.KindCliServiceTests;

public class CreateClusterAsyncTests : KindCliServiceTestsBase
{
    [Fact]
    public async Task CreateClusterAsync_NoClusterNameAndNoClusterConfig_CreatesDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        var result = await _kindCliService.GetClustersAsync();
        Assert.Equal("kind", result.First());
        await _kindCliService.DeleteClusterAsync();
    }

    [Fact]
    public async Task CreateClusterAsync_NoClusterNameAndValidClusterConfig_CreatesCustomClusterWithDefaultName()
    {
        await _kindCliService.CreateClusterAsync(configPath: "assets/kind-config.yaml");
        var result = await _kindCliService.GetClustersAsync();
        Assert.Equal("custom-cluster", result.First());
        await _kindCliService.DeleteClusterAsync("custom-cluster");
    }

    [Fact]
    public async Task CreateClusterAsync_NoClusterNameAndInvalidClusterConfig_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.CreateClusterAsync(configPath: "invalid-path/kind-config.yaml"));
        Assert.Equal("The specified 'configPath': 'invalid-path/kind-config.yaml' does not exist.", ex.Message);
    }

    [Fact]
    public async Task CreateClusterAsync_ValidClusterNameAndNoClusterConfig_CreatesNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        var result = await _kindCliService.GetClustersAsync();
        Assert.Equal("named-cluster", result.First());
        await _kindCliService.DeleteClusterAsync("named-cluster");
    }

    [Fact]
    public async Task CreateClusterAsync_ValidClusterNameAndValidClusterConfig_CreatesCustomCluster()
    {
        await _kindCliService.CreateClusterAsync("custom-cluster", "assets/kind-config.yaml");
        var result = await _kindCliService.GetClustersAsync();
        Assert.Equal("custom-cluster", result.First());
        await _kindCliService.DeleteClusterAsync("custom-cluster");
    }

    [Fact]
    public async Task CreateClusterAsync_ValidClusterNameAndInvalidClusterConfig_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.CreateClusterAsync("custom-cluster", "invalid-path/kind-config.yaml"));
        Assert.Equal("The specified 'configPath': 'invalid-path/kind-config.yaml' does not exist.", ex.Message);
    }

    [Fact]
    public async Task CreateClusterAsync_InvalidClusterNameAndNoClusterConfig_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.CreateClusterAsync("@_~"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }

    [Fact]
    public async Task CreateClusterAsync_InvalidClusterNameAndValidClusterConfig_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.CreateClusterAsync("@_~", "assets/kind-config.yaml"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }

    [Fact]
    public async Task CreateClusterAsync_InvalidClusterNameAndInvalidClusterConfig_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.CreateClusterAsync("@_~", "invalid-path/kind-config.yaml"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }
}
