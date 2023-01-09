namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class ExportLogsAsyncTests : KindCliServiceTestsBase
{
    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndNoLogPath_ExportLogsFromNamedCluster()
    {
        //Arrange
        string clusterName = Guid.NewGuid().ToString();
        string expected = $"assets/logs/{clusterName}-logs";

        //Act
        await _kindCliService.CreateClusterAsync(clusterName);
        _ = await _kindCliService.ExportLogsAsync(clusterName);
        bool actual = Directory.Exists(expected);

        //Assert
        _ = actual.Should().BeTrue();

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
        Directory.Delete(expected, true);
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndLogPath_ExportLogsFromCustomLogPathOnNamedCluster()
    {
        //Arrange
        const string expected = "assets/logs/named-cluster-logs";
        const string logPath = "named-cluster-logs";
        string clusterName = Guid.NewGuid().ToString();

        //Act
        await _kindCliService.CreateClusterAsync(clusterName);
        _ = await _kindCliService.ExportLogsAsync(clusterName, logPath);
        bool actual = Directory.Exists(expected);

        //Assert
        _ = actual.Should().BeTrue();

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
        Directory.Delete(expected, true);
    }

    [Fact]
    public void ExportLogsAsync_InvalidClusterNameAndNoLogPath_Throws()
    {
        //Arrange
        const string clusterName = "@_~";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.ExportLogsAsync(clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public void ExportLogsAsync_InvalidClusterNameAndLogPath_Throws()
    {
        //Arrange
        const string clusterName = "@_~";
        const string logPath = "kind-logs.tar.gz";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.ExportLogsAsync(clusterName, logPath);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }
}
