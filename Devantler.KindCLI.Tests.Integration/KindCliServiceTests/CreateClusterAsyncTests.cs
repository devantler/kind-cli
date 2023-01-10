namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class CreateClusterAsyncTests : KindCliServiceTestsBase
{
    public static IEnumerable<object[]> ValidCases
    {
        get
        {
            yield return new object[] { null!, null! };
            yield return new object[] { Guid.NewGuid().ToString(), "assets/kind-config.yaml" };
            yield return new object[] { Guid.NewGuid().ToString(), null! };
        }
    }

    public static IEnumerable<object[]> InvalidCases
    {
        get
        {
            yield return new object[] { Guid.NewGuid().ToString(), "invalid-path/kind-config.yaml", ErrorMessages.InvalidConfigPath("invalid-path/kind-config.yaml") };
            yield return new object[] { "@_~", null!, ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", "assets/kind-config.yaml", ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", "invalid-path/kind-config.yaml", ErrorMessages.InvalidClusterName("@_~") };
        }
    }

    [Theory]
    [MemberData(nameof(ValidCases))]
    public async Task CreateClusterAsync_ValidParameters_CreatesCluster(string? clusterName, string? configPath)
    {
        //Act
        await _kindCliService.CreateClusterAsync(clusterName, configPath);
        IEnumerable<string> result = await _kindCliService.GetClustersAsync();

        //Assert
        _ = result.Should().Contain(clusterName ?? "kind");

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }

    [Theory]
    [MemberData(nameof(InvalidCases))]
    public void CreateClusterAsync_InvalidParameters_Throws(string? clusterName, string? configPath, string expected)
    {
        //Act
        Func<Task> action = async () => await _kindCliService.CreateClusterAsync(clusterName, configPath);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);

        //Cleanup
        _ = _kindCliService.DeleteClusterAsync(clusterName);
    }
}
