namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public static class ErrorMessages
{
    public static string InvalidClusterName(string clusterName) => $"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'";
    public static string InvalidImageArchivePath(string imageArchivePath) => $"The specified 'imageArchivePath': '{imageArchivePath}' does not exist.";
    internal static object NullReference() => "";
    internal static object EmptyImageList() => "The specified 'imageNames' must contain at least one image name.";
    internal static object InvalidConfigPath(string configPath) => $"The specified 'configPath': '{configPath}' does not exist.";
    internal static object InvalidLogPath(string logPath) => throw new NotImplementedException();
}
