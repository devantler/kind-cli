namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class LoadImageArchiveAsync : KindCliServiceTestsBase
{
    public static IEnumerable<object[]> ValidCases
    {
        get
        {
            yield return new object[] { Guid.NewGuid().ToString(), "assets/nginx.tar" };
        }
    }

    public static IEnumerable<object[]> InvalidCases
    {
        get
        {
            yield return new object[] { "@_~", "assets/nginx.tar", ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", "assets/does-not-exist.tar", ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", null!, ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { Guid.NewGuid().ToString(), null!, ErrorMessages.NullReference() };
            yield return new object[] { Guid.NewGuid().ToString(), "assets/does-not-exist.tar", ErrorMessages.InvalidImageArchivePath("assets/does-not-exist.tar") };
        }
    }

    [Theory]
    [MemberData(nameof(ValidCases))]
    public async Task LoadImageArchiveAsync_ValidParameters_LoadsImageArchiveToNamedCluster(string? clusterName, string imageArchivePath)
    {
        //Arrange
        if (clusterName != "@_~")
            await _kindCliService.CreateClusterAsync(clusterName);

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageArchiveAsync(imageArchivePath, clusterName);

        //Assert
        _ = action.Should().NotThrowAsync();

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }

    [Theory]
    [MemberData(nameof(InvalidCases))]
    public async Task LoadImageArchiveAsync_InvalidParameters_ThrowsAsync(string? clusterName, string imageArchivePath, string expected)
    {
        //Arrange
        if (clusterName != "@_~")
            await _kindCliService.CreateClusterAsync(clusterName);

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageArchiveAsync(imageArchivePath, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);

        //Cleanup
        if (clusterName != "@_~")
            _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }
}
