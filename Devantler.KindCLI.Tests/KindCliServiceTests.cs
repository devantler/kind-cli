using System;
using System.Linq;
using System.Threading.Tasks;
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

    }

    [Fact]
    public async Task CreateClusterAsync_ValidClusterNameAndInvalidClusterConfig_ThrowsInvalidClusterConfigException()
    {
    }

    [Fact]
    public async Task CreateClusterAsync_InvalidClusterNameAndNoClusterConfig_ThrowsInvalidClusterNameException()
    {
    }

    [Fact]
    public async Task CreateClusterAsync_InvalidClusterNameAndValidClusterConfig_ThrowsInvalidClusterNameException()
    {
    }

    [Fact]
    public async Task CreateClusterAsync_InvalidClusterNameAndInvalidClusterConfig_ThrowsInvalidClusterNameException()
    {
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
    }

    [Fact]
    public async Task DeleteClusterAsync_InvalidClusterName_ThrowsInvalidClusterNameException()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_NoClusterNameAndNoLogPath_ExportLogsFromDefaultCluster()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_NoClusterNameAndValidLogPath_ExportLogsFromCustomLogPathOnDefaultCluster()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_NoClusterNameAndInvalidLogPath_Throws()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndNoLogPath_ExportLogsFromNamedCluster()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndValidLogPath_ExportLogsFromCustomLogPathOnNamedCluster()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_ValidClusterNameAndInvalidLogPath_Throws()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_InvalidClusterNameAndNoLogPath_Throws()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_InvalidClusterNameAndValidLogPath_Throws()
    {
    }

    [Fact]
    public async Task ExportLogsAsync_InvalidClusterNameAndInvalidLogPath_Throws()
    {
    }

    [Fact]
    public async Task GetClusterInfoAsync_NoClusterName_GetsClusterInfoForDefaultCluster()
    {
    }

    [Fact]
    public async Task GetClusterInfoAsync_ValidClusterName_GetsClusterInfoForNamedCluster()
    {
    }

    [Fact]
    public async Task GetClusterInfoAsync_InvalidClusterName_Throws()
    {
    }

    [Fact]
    public async Task GetClustersAsync_NoClusters_ReturnsEmptyList()
    {
    }

    [Fact]
    public async Task GetClustersAsync_MultipleClusters_ReturnsListOfClusterNames()
    {
    }

    [Fact]
    public async Task LoadImageArchiveAsync_NoClusterNameAndValidImageArchivePath_LoadsImageArchiveToDefaultCluster()
    {
    }

    [Fact]
    public async Task LoadImageArchiveAsync_NoClusterNameAndInvalidImageArchivePath_Throws()
    {
    }

    [Fact]
    public async Task LoadImageArchiveAsync_ValidClusterNameAndValidImageArchivePath_LoadsImageArchiveToNamedCluster()
    {
    }

    [Fact]
    public async Task LoadImageArchiveAsync_ValidClusterNameAndInvalidImageArchivePath_Throws()
    {
    }

    [Fact]
    public async Task LoadImageArchiveAsync_InvalidClusterNameAndValidImageArchivePath_Throws()
    {
    }

    [Fact]
    public async Task LoadImageArchiveAsync_InvalidClusterNameAndInvalidImageArchivePath_Throws()
    {
    }

    [Fact]
    public async Task LoadImageAsync_NoClusterNameAndEmptyImageNames_DoesNothing()
    {
    }

    [Fact]
    public async Task LoadImageAsync_NoClusterNameAndInvalidImageNames_Throws()
    {
    }

    [Fact]
    public async Task LoadImageAsync_NoClusterNameAndValidImageNames_LoadsImagesToDefaultCluster()
    {
    }

    [Fact]
    public async Task LoadImageAsync_ValidClusterNameAndEmptyImageNames_DoesNothing()
    {
    }

    [Fact]
    public async Task LoadImageAsync_ValidClusterNameAndInvalidImageNames_Throws()
    {
    }

    [Fact]
    public async Task LoadImageAsync_ValidClusterNameAndValidImageNames_LoadsImagesToNamedCluster()
    {
    }

    [Fact]
    public async Task LoadImageAsync_InvalidClusterNameAndEmptyImageNames_Throws()
    {
    }

    [Fact]
    public async Task LoadImageAsync_InvalidClusterNameAndValidImageNames_Throws()
    {
    }

    [Fact]
    public async Task LoadImageAsync_InvalidClusterNameAndInvalidImageNames_Throws()
    {
    }
}