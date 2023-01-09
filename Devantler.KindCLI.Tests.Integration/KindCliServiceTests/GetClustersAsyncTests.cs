namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class GetClustersAsyncTests : KindCliServiceTestsBase
{
    [Fact]
    public async Task GetClustersAsync_NoClusters_ReturnsEmptyList()
    {
        //Act
        IEnumerable<string> actual = await _kindCliService.GetClustersAsync();

        //Assert
        _ = actual.Should().BeEmpty();
    }

    [Fact]
    public async Task GetClustersAsync_MultipleClusters_ReturnsListOfClusterNames()
    {
        //Arrange
        string clusterName1 = Guid.NewGuid().ToString();
        string clusterName2 = Guid.NewGuid().ToString();

        //Act
        await _kindCliService.CreateClusterAsync(clusterName1);
        await _kindCliService.CreateClusterAsync(clusterName2);
        IEnumerable<string> actual = await _kindCliService.GetClustersAsync();

        //Assert
        _ = actual.Should().HaveCountGreaterThanOrEqualTo(2);
        _ = actual.Should().Contain(clusterName1);
        _ = actual.Should().Contain(clusterName2);

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName1);
        _ = await _kindCliService.DeleteClusterAsync(clusterName2);
    }
}
