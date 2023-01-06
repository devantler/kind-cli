namespace Devantler.KindCLI.Tests.KindCliServiceTests;

public class LoadImageAsync : KindCliServiceTestsBase
{
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
        await _kindCliService.LoadImageAsync(new List<string> { "nginx:alpine" });
        await _kindCliService.DeleteClusterAsync();
    }

    [Fact]
    public async Task LoadImageAsync_ValidImageNamesAndValidClusterName_LoadsImagesToNamedCluster()
    {
        await _kindCliService.CreateClusterAsync("named-cluster");
        await _kindCliService.LoadImageAsync(new List<string> { "nginx:alpine" }, "named-cluster");
        await _kindCliService.DeleteClusterAsync("named-cluster");
    }

    [Fact]
    public async Task LoadImageAsync_ValidImageNamesAndInvalidClusterName_Throws()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await _kindCliService.LoadImageAsync(new List<string> { "nginx:alpine" }, "@_~"));
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
