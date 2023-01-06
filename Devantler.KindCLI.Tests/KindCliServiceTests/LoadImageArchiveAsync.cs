namespace Devantler.KindCLI.Tests.KindCliServiceTests;

public class LoadImageArchiveAsync : KindCliServiceTestsBase
{
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
}
