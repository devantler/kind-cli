namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class ExportLogsAsyncTests : KindCliServiceTestsBase
{
    public static IEnumerable<object[]> ValidCases
    {
        get
        {
            yield return new object[] { Guid.NewGuid().ToString(), null! };
            yield return new object[] { Guid.NewGuid().ToString(), "assets/logs/custom-logs" };
        }
    }

    public static IEnumerable<object[]> InvalidCases
    {
        get
        {
            yield return new object[] { "@_~", null!, ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", "assets/logs/custom-logs", ErrorMessages.InvalidClusterName("@_~") };
            yield return new object[] { "@_~", "@_~", ErrorMessages.InvalidClusterName("@_~") };
            // yield return new object[] { Guid.NewGuid().ToString(), "@_~", ErrorMessages.InvalidLogPath("@_~") };
        }
    }

    [Theory]
    [MemberData(nameof(ValidCases))]
    public async Task ExportLogsAsync_ValidParameters_ExportLogsFromCluster(string? clusterName, string? logPath)
    {
        //Arrange
        string expected = logPath ?? $"assets/logs/{clusterName}-logs";
        await _kindCliService.CreateClusterAsync(clusterName);

        //Act
        _ = await _kindCliService.ExportLogsAsync(clusterName, expected);
        bool actual = Directory.Exists(expected);

        //Assert
        _ = actual.Should().BeTrue();

        //Cleanup
        _ = await _kindCliService.DeleteClusterAsync(clusterName);
        Directory.Delete(expected, true);
    }

    [Theory]
    [MemberData(nameof(InvalidCases))]
    public async Task ExportLogsAsync_InvalidParameters_ThrowsAsync(string? clusterName, string? configPath, string expected)
    {
        //Arrange
        if (clusterName != "@_~")
            await _kindCliService.CreateClusterAsync(clusterName);

        //Act
        Func<Task> action = async () => await _kindCliService.ExportLogsAsync(clusterName, configPath);

        //Assert
        _ = action.Should().ThrowAsync<ArgumentException>().WithMessage(expected);

        //Cleanup
        if (clusterName != "@_~")
            _ = await _kindCliService.DeleteClusterAsync(clusterName);
    }
}
