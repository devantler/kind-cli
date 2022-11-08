using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using Xunit;

namespace Devantler.KindCLI.Tests;

public class KindCliServiceTests
{
    private readonly IKindCliService _kindCliService;

    public KindCliServiceTests()
    {
        _kindCliService = new KindCliService();
    }

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

    [Fact]
    public async Task ExportLogsAsync_NoClusterNameAndNoLogPath_ExportLogsFromDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        await _kindCliService.ExportLogsAsync();
        Assert.True(System.IO.Directory.Exists("assets/logs/kind-logs"));
        await _kindCliService.DeleteClusterAsync();
        System.IO.Directory.Delete("assets/logs/kind-logs", true);
    }

    [Fact]
    public async Task ExportLogsAsync_NoClusterNameAndLogPath_ExportLogsFromCustomLogPathOnDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        await _kindCliService.ExportLogsAsync(logPath: "kind-logs");
        Assert.True(System.IO.Directory.Exists("assets/logs/kind-logs"));
        await _kindCliService.DeleteClusterAsync();
        System.IO.Directory.Delete("assets/logs/kind-logs", true);
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndNoLogPath_ExportLogsFromNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.ExportLogsAsync("named-cluster");
        Assert.True(System.IO.Directory.Exists("assets/logs/named-cluster-logs"));
        await _kindCliService.DeleteClusterAsync("named-cluster");
        System.IO.Directory.Delete("assets/logs/named-cluster-logs", true);
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndLogPath_ExportLogsFromCustomLogPathOnNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.ExportLogsAsync("named-cluster", "named-cluster-logs");
        Assert.True(System.IO.Directory.Exists("assets/logs/named-cluster-logs"));
        await _kindCliService.DeleteClusterAsync("named-cluster");
        System.IO.Directory.Delete("assets/logs/named-cluster-logs", true);
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

    [Fact]
    public async Task GetClustersAsync_NoClusters_ReturnsEmptyList()
    {
        Assert.Empty(await _kindCliService.GetClustersAsync());
    }

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

    [Fact]
    public async Task LoadImageArchiveAsync_ValidImageArchivePathAndNoClusterName_LoadsImageArchiveToDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        await _kindCliService.LoadImageArchiveAsync("assets/nginx.tar");
        await _kindCliService.DeleteClusterAsync();
    }

    [Fact]
    public async Task LoadImageArchiveAsync_ValidImageArchivePathAndValidClusterName_LoadsImageArchiveToNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.LoadImageArchiveAsync("assets/nginx.tar", "named-cluster");
        await _kindCliService.DeleteClusterAsync("named-cluster");
    }

    [Fact]
    public async Task LoadImageArchiveAsync_ValidImageArchivePathAndInvalidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageArchiveAsync("assets/nginx.tar", "@_~"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }

    [Fact]
    public async Task LoadImageArchiveAsync_InvalidImageArchivePathAndNoClusterName_Throws()
    {
        await _kindCliService.CreateClusterAsync();
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageArchiveAsync("invalid-path/nginx.tar"));
        Assert.Equal("The specified 'imageArchivePath': 'invalid-path/nginx.tar' does not exist.", ex.Message);
        await _kindCliService.DeleteClusterAsync();
    }

    [Fact]
    public async Task LoadImageArchiveAsync_InvalidImageArchivePathAndValidClusterName_Throws()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageArchiveAsync("invalid-path/nginx.tar", "named-cluster"));
        Assert.Equal("The specified 'imageArchivePath': 'invalid-path/nginx.tar' does not exist.", ex.Message);
        await _kindCliService.DeleteClusterAsync("named-cluster");
    }

    [Fact]
    public async Task LoadImageArchiveAsync_InvalidImageArchivePathAndInvalidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageArchiveAsync("invalid-path/nginx.tar", "@_~"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }

    [Fact]
    public async Task LoadImageAsync_EmptyImageNamesAndNoClusterName_Throws()
    {
        await _kindCliService.CreateClusterAsync();
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageAsync(new List<string>()));
        Assert.Equal("The specified 'imageNames' must contain at least one image name.", ex.Message);
        await _kindCliService.DeleteClusterAsync();
    }

    [Fact]
    public async Task LoadImageAsync_EmptyImageNamesAndValidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageAsync(new List<string>(), "named-cluster"));
        Assert.Equal("The specified 'imageNames' must contain at least one image name.", ex.Message);
    }

    [Fact]
    public async Task LoadImageAsync_EmptyImageNamesAndInvalidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageAsync(new List<string>(), "@_~"));
        Assert.Equal("The specified 'imageNames' must contain at least one image name.", ex.Message);
    }

    [Fact]
    public async Task LoadImageAsync_ValidImageNamesAndNoClusterName_LoadsImagesToDefaultCluster()
    {
        await _kindCliService.CreateClusterAsync();
        await _kindCliService.LoadImageAsync(new List<string> { "nginx:latest" });
        await _kindCliService.DeleteClusterAsync();
    }

    [Fact]
    public async Task LoadImageAsync_ValidImageNamesAndValidClusterName_LoadsImagesToNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.LoadImageAsync(new List<string> { "nginx:latest" }, "named-cluster");
        await _kindCliService.DeleteClusterAsync("named-cluster");
    }

    [Fact]
    public async Task LoadImageAsync_ValidImageNamesAndInvalidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageAsync(new List<string> { "nginx:latest" }, "@_~"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }

    [Fact]
    public async Task LoadImageAsync_InvalidImageNamesAndNoClusterName_Fails()
    {
        await _kindCliService.CreateClusterAsync();
        var ex = await Assert.ThrowsAsync<Exception>(async () => await _kindCliService.LoadImageAsync(new List<string> { "invalid-image-name" }));
        Assert.Equal("Failed to load images: 'invalid-image-name'", ex.Message);
        await _kindCliService.DeleteClusterAsync();
    }

    [Fact]
    public async Task LoadImageAsync_InvalidImageNamesAndValidClusterName_Throws()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        var ex = await Assert.ThrowsAsync<Exception>(async () => await _kindCliService.LoadImageAsync(new List<string> { "invalid-image-name" }, "named-cluster"));
        Assert.Equal("Failed to load images: 'invalid-image-name'", ex.Message);
        await _kindCliService.DeleteClusterAsync("named-cluster");
    }

    [Fact]
    public async Task LoadImageAsync_InvalidImageNamesAndInvalidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageAsync(new List<string> { "invalid-image-name" }, "@_~"));
        Assert.Equal("The specified 'clusterName': '@_~' is invalid. It must match '^[a-z0-9.-]+$'", ex.Message);
    }
}