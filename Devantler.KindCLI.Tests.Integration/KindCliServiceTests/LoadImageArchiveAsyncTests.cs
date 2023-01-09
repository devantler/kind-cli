namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class LoadImageArchiveAsync : KindCliServiceTestsBase
{
    [Fact]
    public async Task LoadImageArchiveAsync_ValidImageArchivePathAndValidClusterName_LoadsImageArchiveToNamedCluster()
    {
        //Arrange
        const string imageArchivePath = "assets/nginx.tar";
        string clusterName = Guid.NewGuid().ToString();

        //Act
        await _kindCliService.CreateClusterAsync(clusterName);
        Func<Task> action = async () => await _kindCliService.LoadImageArchiveAsync(imageArchivePath, clusterName);

        //Assert
        _ = action.Should().NotThrowAsync();

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }

    [Fact]
    public void LoadImageArchiveAsync_ValidImageArchivePathAndInvalidClusterName_Throws()
    {
        //Arrange
        const string imageArchivePath = "assets/nginx.tar";
        const string clusterName = "@_~";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageArchiveAsync(imageArchivePath, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public async Task LoadImageArchiveAsync_InvalidImageArchivePathAndValidClusterName_Throws()
    {
        //Arrange
        const string imageArchivePath = "invalid-path/nginx.tar";
        string clusterName = Guid.NewGuid().ToString();
        const string expected = $"The specified 'imageArchivePath': '{imageArchivePath}' does not exist.";

        //Act
        await _kindCliService.CreateClusterAsync(clusterName);
        Func<Task> action = async () => await _kindCliService.LoadImageArchiveAsync(imageArchivePath, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }

    [Fact]
    public void LoadImageArchiveAsync_InvalidImageArchivePathAndInvalidClusterName_Throws()
    {
        //Arrange
        const string imageArchivePath = "invalid-path/nginx.tar";
        const string clusterName = "@_~";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageArchiveAsync(imageArchivePath, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }
}
