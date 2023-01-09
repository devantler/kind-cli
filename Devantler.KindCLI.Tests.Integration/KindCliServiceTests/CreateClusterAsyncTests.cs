namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class CreateClusterAsyncTests : KindCliServiceTestsBase
{
    [Fact]
    public async Task CreateClusterAsync_NoClusterName_CreatesDefaultCluster()
    {
        //Arrange
        const string expected = "kind";

        //Act
        await _kindCliService.CreateClusterAsync();
        IEnumerable<string> result = await _kindCliService.GetClustersAsync();

        //Assert
        _ = result.Should().Contain(expected);

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(expected);
    }

    [Fact]
    public async Task CreateClusterAsync_ClusterNameAndValidClusterConfig_CreatesCustomClusterWithDefaultName()
    {
        //Arrange
        string expected = Guid.NewGuid().ToString();
        const string configPath = "assets/kind-config.yaml";

        //Act
        await _kindCliService.CreateClusterAsync(expected, configPath);
        IEnumerable<string> result = await _kindCliService.GetClustersAsync();

        //Assert
        _ = result.Should().Contain(expected);

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(expected);
    }

    [Fact]
    public void CreateClusterAsync_ClusterNameAndInvalidClusterConfig_Throws()
    {
        //Arrange
        const string configPath = "invalid-path/kind-config.yaml";
        const string expected = $"The specified 'configPath': '{configPath}' does not exist.";
        string clusterName = Guid.NewGuid().ToString();

        //Act
        Func<Task> action = async () => await _kindCliService.CreateClusterAsync(clusterName, configPath);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public async Task CreateClusterAsync_ValidClusterNameAndNoClusterConfig_CreatesNamedCluster()
    {
        //Arrange
        string expected = Guid.NewGuid().ToString();

        //Act
        await _kindCliService.CreateClusterAsync(expected);
        IEnumerable<string> result = await _kindCliService.GetClustersAsync();

        //Assert
        _ = result.Should().Contain(expected);

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(expected);
    }

    [Fact]
    public async Task CreateClusterAsync_ValidClusterNameAndValidClusterConfig_CreatesCustomCluster()
    {
        //Arrange
        const string configPath = "assets/kind-config.yaml";
        string expected = Guid.NewGuid().ToString();

        //Act
        await _kindCliService.CreateClusterAsync(expected, configPath);
        IEnumerable<string> result = await _kindCliService.GetClustersAsync();

        //Assert
        _ = result.Should().Contain(expected);

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(expected);
    }

    [Fact]
    public void CreateClusterAsync_ValidClusterNameAndInvalidClusterConfig_Throws()
    {
        //Arrange
        const string configPath = "invalid-path/kind-config.yaml";
        const string expected = $"The specified 'configPath': '{configPath}' does not exist.";
        string clusterName = Guid.NewGuid().ToString();

        //Act
        Func<Task> action = async () => await _kindCliService.CreateClusterAsync(clusterName, configPath);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public void CreateClusterAsync_InvalidClusterNameAndNoClusterConfig_Throws()
    {
        //Arrange
        const string clusterName = "@_~";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.CreateClusterAsync(clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public void CreateClusterAsync_InvalidClusterNameAndValidClusterConfig_Throws()
    {
        //Arrange
        const string clusterName = "@_~";
        const string configPath = "assets/kind-config.yaml";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.CreateClusterAsync(clusterName, configPath);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public void CreateClusterAsync_InvalidClusterNameAndInvalidClusterConfig_Throws()
    {
        //Arrange
        const string clusterName = "@_~";
        const string configPath = "invalid-path/kind-config.yaml";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.CreateClusterAsync(clusterName, configPath);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }
}
