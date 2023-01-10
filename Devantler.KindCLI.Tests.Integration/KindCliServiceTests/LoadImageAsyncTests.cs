using System.Collections.ObjectModel;

namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class LoadImageAsync : KindCliServiceTestsBase
{
    public static IEnumerable<object[]> ValidCases
    {
        get
        {
            yield return new object[] { Guid.NewGuid().ToString(), new Collection<string> { "nginx:alpine" } };
        }
    }

    public static IEnumerable<object[]> InvalidCases
    {
        get
        {
            yield return new object[] { Guid.NewGuid().ToString(), null!, ErrorMessages.NullReference() };
            yield return new object[] { Guid.NewGuid().ToString(), new Collection<string>(), ErrorMessages.EmptyImageList() };
            yield return new object[] { "@_~", null!, ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", new Collection<string>(), ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", new Collection<string> { "nginx:alpine" }, ErrorMessages.InvalidClusterName("@_~") };
        }
    }

    [Theory]
    [MemberData(nameof(ValidCases))]
    public async void LoadImageAsync_ValidParameters_LoadsImageIntoCluster(string clusterName, Collection<string> imageNames)
    {
        //Arrange
        await _kindCliService.CreateClusterAsync(clusterName);

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(imageNames, clusterName);

        //Assert
        _ = action.Should().NotThrowAsync();

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }

    [Theory]
    [MemberData(nameof(InvalidCases))]
    public async void LoadImageAsync_InvalidParameters_Throws(string clusterName, Collection<string>? imageNames, string expected)
    {
        //Arrange
        if (clusterName != "@_~")
            await _kindCliService.CreateClusterAsync(clusterName);

        //Act
        Func<Task> action = async () => await _kindCliService.LoadImageAsync(imageNames!, clusterName);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);

        //Cleanup
        if (clusterName != "@_~")
            _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }
}
