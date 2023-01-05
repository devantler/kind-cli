namespace Devantler.KindCLI.Tests.KindCliServiceTests;

public class GetClustersAsync : KindCliServiceTestsBase
{
    [Fact]
    public async Task GetClustersAsync_NoClusters_ReturnsEmptyList() =>
        Assert.Empty(await _kindCliService.GetClustersAsync());

    [Fact]
    public async Task GetClustersAsync_MultipleClusters_ReturnsListOfClusterNames()
    {
        await _kindCliService.CreateClusterAsync("cluster-1");
        await _kindCliService.CreateClusterAsync("cluster-2");
        var clusters = await _kindCliService.GetClustersAsync();
        Assert.Equal(2, clusters.ToList().Count);
        Assert.Contains("cluster-1", clusters);
        Assert.Contains("cluster-2", clusters);
        await _kindCliService.DeleteClusterAsync("cluster-1");
        await _kindCliService.DeleteClusterAsync("cluster-2");
    }
}
