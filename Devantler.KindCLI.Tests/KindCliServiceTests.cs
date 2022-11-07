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
    public void CreateClusterAsync_NoClusterNameAndValidClusterConfig_CreatesCustomClusterWithDefaultName()
    {
    }

    [Fact]
    public void CreateClusterAsync_ValidClusterNameAndNoClusterConfig_CreatesNamedCluster()
    {
    }

    [Fact]
    public void CreateClusterAsync_ValidClusterNameAndValidClusterConfig_CreatesCustomCluster()
    {
    }

    [Fact]
    public void CreateClusterAsync_ValidClusterNameAndInvalidClusterConfig_ThrowsInvalidClusterConfigException()
    {
    }

    [Fact]
    public void CreateClusterAsync_InvalidClusterNameAndNoClusterConfig_ThrowsInvalidClusterNameException()
    {
    }

    [Fact]
    public void CreateClusterAsync_InvalidClusterNameAndValidClusterConfig_ThrowsInvalidClusterNameException()
    {
    }

    [Fact]
    public void CreateClusterAsync_InvalidClusterNameAndInvalidClusterConfig_ThrowsInvalidClusterNameException()
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
    public void DeleteClusterAsync_ValidClusterName_DeletesNamedCluster()
    {
    }

    [Fact]
    public void DeleteClusterAsync_InvalidClusterName_ThrowsInvalidClusterNameException()
    {
    }

    [Fact]
    public void ExportLogsAsync_NoClusterNameAndNoLogPath_ExportLogsFromDefaultCluster()
    {
    }

    [Fact]
    public void ExportLogsAsync_NoClusterNameAndValidLogPath_ExportLogsFromCustomLogPathOnDefaultCluster()
    {
    }

    [Fact]
    public void ExportLogsAsync_NoClusterNameAndInvalidLogPath_Throws()
    {
    }

    [Fact]
    public void ExportLogsAsync_ValidClusterNameAndNoLogPath_ExportLogsFromNamedCluster()
    {
    }

    [Fact]
    public void ExportLogsAsync_ValidClusterNameAndValidLogPath_ExportLogsFromCustomLogPathOnNamedCluster()
    {
    }

    [Fact]
    public void ExportLogsAsync_ValidClusterNameAndInvalidLogPath_Throws()
    {
    }

    [Fact]
    public void ExportLogsAsync_InvalidClusterNameAndNoLogPath_Throws()
    {
    }

    [Fact]
    public void ExportLogsAsync_InvalidClusterNameAndValidLogPath_Throws()
    {
    }

    [Fact]
    public void ExportLogsAsync_InvalidClusterNameAndInvalidLogPath_Throws()
    {
    }

    [Fact]
    public void GetClusterInfoAsync_NoClusterName_GetsClusterInfoForDefaultCluster()
    {
    }

    [Fact]
    public void GetClusterInfoAsync_ValidClusterName_GetsClusterInfoForNamedCluster()
    {
    }

    [Fact]
    public void GetClusterInfoAsync_InvalidClusterName_Throws()
    {
    }

    [Fact]
    public void GetClustersAsync_NoClusters_ReturnsEmptyList()
    {
    }

    [Fact]
    public void GetClustersAsync_MultipleClusters_ReturnsListOfClusterNames()
    {
    }

    [Fact]
    public void LoadImageArchiveAsync_NoClusterNameAndValidImageArchivePath_LoadsImageArchiveToDefaultCluster()
    {
    }

    [Fact]
    public void LoadImageArchiveAsync_NoClusterNameAndInvalidImageArchivePath_Throws()
    {
    }

    [Fact]
    public void LoadImageArchiveAsync_ValidClusterNameAndValidImageArchivePath_LoadsImageArchiveToNamedCluster()
    {
    }

    [Fact]
    public void LoadImageArchiveAsync_ValidClusterNameAndInvalidImageArchivePath_Throws()
    {
    }

    [Fact]
    public void LoadImageArchiveAsync_InvalidClusterNameAndValidImageArchivePath_Throws()
    {
    }

    [Fact]
    public void LoadImageArchiveAsync_InvalidClusterNameAndInvalidImageArchivePath_Throws()
    {
    }

    [Fact]
    public void LoadImageAsync_NoClusterNameAndEmptyImageNames_DoesNothing()
    {
    }

    [Fact]
    public void LoadImageAsync_NoClusterNameAndInvalidImageNames_Throws()
    {
    }

    [Fact]
    public void LoadImageAsync_NoClusterNameAndValidImageNames_LoadsImagesToDefaultCluster()
    {
    }

    [Fact]
    public void LoadImageAsync_ValidClusterNameAndEmptyImageNames_DoesNothing()
    {
    }

    [Fact]
    public void LoadImageAsync_ValidClusterNameAndInvalidImageNames_Throws()
    {
    }

    [Fact]
    public void LoadImageAsync_ValidClusterNameAndValidImageNames_LoadsImagesToNamedCluster()
    {
    }

    [Fact]
    public void LoadImageAsync_InvalidClusterNameAndEmptyImageNames_Throws()
    {
    }

    [Fact]
    public void LoadImageAsync_InvalidClusterNameAndValidImageNames_Throws()
    {
    }

    [Fact]
    public void LoadImageAsync_InvalidClusterNameAndInvalidImageNames_Throws()
    {
    }
}