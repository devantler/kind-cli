using System.Collections.ObjectModel;

namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class LoadImageAsync : KindCliServiceTestsBase
{
    [Fact]
    public void LoadImageAsync_EmptyImageNamesAndValidClusterName_Throws()
    {
        //Arrange
        string clusterName = Guid.NewGuid().ToString();
        const string expected = "The specified 'imageNames' must contain at least one image name.";

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(new Collection<string>(), clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public void LoadImageAsync_EmptyImageNamesAndInvalidClusterName_Throws()
    {
        //Arrange
        const string clusterName = "@_~";
        const string expected = "The specified 'imageNames' must contain at least one image name.";

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(new Collection<string>(), clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public async Task LoadImageAsync_ValidImageNamesAndValidClusterName_LoadsImagesToNamedCluster()
    {
        //Arrange
        const string imageName = "nginx:alpine";
        string clusterName = Guid.NewGuid().ToString();

        //Act
        await _kindCliService.CreateClusterAsync(clusterName);
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(new Collection<string> { imageName }, clusterName);

        //Assert
        _ = action.Should().NotThrowAsync();

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }

    [Fact]
    public void LoadImageAsync_ValidImageNamesAndInvalidClusterName_Throws()
    {
        //Arrange
        const string imageName = "nginx:alpine";
        const string clusterName = "@_~";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(new Collection<string> { imageName }, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }

    [Fact]
    public async Task LoadImageAsync_InvalidImageNamesAndValidClusterName_Throws()
    {
        //Arrange
        string clusterName = Guid.NewGuid().ToString();
        const string imageName = "invalid-image-name";
        const string expected = $"Failed to load images: '{imageName}'";

        //Act
        await _kindCliService.CreateClusterAsync(clusterName);
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(new Collection<string> { imageName }, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<Exception>().WithMessage(expected);

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync("named-cluster");
    }

    [Fact]
    public void LoadImageAsync_InvalidImageNamesAndInvalidClusterName_Throws()
    {
        //Arrange
        const string clusterName = "@_~";
        const string imageName = "invalid-image-name";
        const string expected = $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(new Collection<string> { imageName }, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);
    }
}
