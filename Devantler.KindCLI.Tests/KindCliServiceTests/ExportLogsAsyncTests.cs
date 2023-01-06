namespace Devantler.KindCLI.Tests.KindCliServiceTests;

public class ExportLogsAsyncTests : KindCliServiceTestsBase
{
    [Fact]
    public async Task ExportLogsAsync_NoClusterNameAndNoLogPath_ExportLogsFromDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        await _kindCliService.ExportLogsAsync();
        Assert.True(Directory.Exists("assets/logs/kind-logs"));
        await _kindCliService.DeleteClusterAsync();
        Directory.Delete("assets/logs/kind-logs", true);
    }

    [Fact]
    public async Task ExportLogsAsync_NoClusterNameAndLogPath_ExportLogsFromCustomLogPathOnDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        await _kindCliService.ExportLogsAsync(logPath: "kind-logs");
        Assert.True(Directory.Exists("assets/logs/kind-logs"));
        await _kindCliService.DeleteClusterAsync();
        Directory.Delete("assets/logs/kind-logs", true);
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndNoLogPath_ExportLogsFromNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.ExportLogsAsync("named-cluster");
        Assert.True(Directory.Exists("assets/logs/named-cluster-logs"));
        await _kindCliService.DeleteClusterAsync("named-cluster");
        Directory.Delete("assets/logs/named-cluster-logs", true);
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndLogPath_ExportLogsFromCustomLogPathOnNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.ExportLogsAsync("named-cluster", "named-cluster-logs");
        Assert.True(Directory.Exists("assets/logs/named-cluster-logs"));
        await _kindCliService.DeleteClusterAsync("named-cluster");
        Directory.Delete("assets/logs/named-cluster-logs", true);
    }

    [Fact]
    public async Task ExportLogsAsync_InvalidClusterNameAndNoLogPath_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.ExportLogsAsync("@_~"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }

    [Fact]
    public async Task ExportLogsAsync_InvalidClusterNameAndLogPath_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.ExportLogsAsync("@_~", "kind-logs.tar.gz"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }
}
