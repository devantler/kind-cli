using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using CliWrap;
using CliWrap.Buffered;

namespace Devantler.KindCLI;

public partial class KindCliService : IKindCliService
{
    public Command KindCli
    {
        get
        {
            var binary = (Environment.OSVersion.Platform, RuntimeInformation.ProcessArchitecture, RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) switch
            {
                (PlatformID.Win32NT, Architecture.X64, _) => "kind-windows-amd64",
                (PlatformID.Unix, Architecture.X64, true) => "kind-darwin-amd64",
                (PlatformID.Unix, Architecture.Arm64, true) => "kind-darwin-arm64",
                (PlatformID.Unix, Architecture.X64, false) => "kind-linux-amd64",
                (PlatformID.Unix, Architecture.Arm64, false) => "kind-linux-arm64",
                (PlatformID.Unix, Architecture.S390x, _) => "kind-linux-s390x",
                _ => throw new PlatformNotSupportedException()
            };
            return Cli.Wrap($"assets/{binary}");
        }
    }

    [GeneratedRegex("^[a-z0-9.-]+$")]
    private static partial Regex ClusterNameRegex();

    public async Task CreateClusterAsync(string? clusterName = default, string? configPath = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        if (configPath != default && !File.Exists(configPath))
            throw new ArgumentException($"The specified 'configPath': '{configPath}' does not exist.");

        var command = (clusterName, configPath) switch
        {
            (null, null) => KindCli.WithArguments("create cluster"),
            (null, _) => KindCli.WithArguments($"create cluster --config {configPath}"),
            (_, null) => KindCli.WithArguments($"create cluster --name {clusterName}"),
            _ => KindCli.WithArguments($"create cluster --name {clusterName} --config {configPath}")
        };
        await command.ExecuteBufferedAsync();
    }

    public async Task<CommandResult> DeleteClusterAsync(string? clusterName = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        var command = clusterName switch
        {
            null => KindCli.WithArguments("delete cluster"),
            _ => KindCli.WithArguments($"delete cluster --name {clusterName}")
        };
        return await command.ExecuteBufferedAsync();
    }

    public async Task<CommandResult> ExportLogsAsync(string? clusterName = default, string? logPath = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        var command = (clusterName, logPath) switch
        {
            (null, null) => KindCli.WithArguments("export logs ./assets/logs/kind-logs"),
            (null, _) => KindCli.WithArguments($"export logs ./assets/logs/{logPath}"),
            (_, null) => KindCli.WithArguments($"export logs ./assets/logs/{clusterName}-logs --name {clusterName}"),
            _ => KindCli.WithArguments($"export logs ./assets/logs/{logPath} --name {clusterName}")
        };
        return await command.ExecuteBufferedAsync();
    }

    public async Task<IEnumerable<string>> GetClustersAsync()
    {
        var command = KindCli.WithArguments("get clusters");
        var result = await command.ExecuteBufferedAsync();
        var clusters = result.StandardOutput.Split(Environment.NewLine).ToList();
        clusters.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        return clusters;
    }

    public async Task<CommandResult> LoadImageArchiveAsync(string imageArchivePath, string? clusterName = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        if (!File.Exists(imageArchivePath))
            throw new ArgumentException($"The specified 'imageArchivePath': '{imageArchivePath}' does not exist.");
        var command = clusterName switch
        {
            null => KindCli.WithArguments($"load image-archive {imageArchivePath}"),
            _ => KindCli.WithArguments($"load image-archive {imageArchivePath} --name {clusterName}")
        };
        return await command.ExecuteBufferedAsync();
    }

    public async Task<CommandResult> LoadImageAsync(List<string> imageNames, string? clusterName = default)
    {
        if (imageNames.Count == 0)
            throw new ArgumentException("The specified 'imageNames' must contain at least one image name.");
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        var command = clusterName switch
        {
            null => KindCli.WithArguments($"load docker-image {string.Join(' ', imageNames)}"),
            _ => KindCli.WithArguments($"load docker-image {string.Join(' ', imageNames)} --name {clusterName}")
        };
        try
        {
            return await command.ExecuteBufferedAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to load images: '{string.Join(' ', imageNames)}'", ex);
        }
    }
}
