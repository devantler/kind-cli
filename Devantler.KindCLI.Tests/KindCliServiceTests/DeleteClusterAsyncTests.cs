namespace Devantler.KindCLI.Tests.KindCliServiceTests;

public class DeleteClusterAsyncTests : KindCliServiceTestsBase
{
    [Fact]
    public async Task DeleteClusterAsync_NoClusterName_DeletesDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        await _kindCliService.DeleteClusterAsync();
        Assert.Empty(await _kindCliService.GetClustersAsync());
    }

    [Fact]
    public async Task DeleteClusterAsync_ValidClusterName_DeletesNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.DeleteClusterAsync("named-cluster");
        Assert.Empty(await _kindCliService.GetClustersAsync());
    }

    [Fact]
    public async Task DeleteClusterAsync_InvalidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.DeleteClusterAsync("@_~"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }
}
