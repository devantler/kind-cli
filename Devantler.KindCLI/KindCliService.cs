using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using CliWrap;
using CliWrap.Buffered;
using CliWrap.Exceptions;

namespace Devantler.KindCLI;

/// <summary>
/// A service that provides access to the Kind CLI.
/// </summary>
public partial class KindCliService : IKindCliService
{
    /// <inheritdoc/>
    public Command KindCli
    {
        get
        {
            string binary = (Environment.OSVersion.Platform, RuntimeInformation.ProcessArchitecture, RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) switch
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

    /// <inheritdoc/>
    public async Task CreateClusterAsync(string? clusterName = default, string? configPath = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        if (configPath != default && !File.Exists(configPath))
            throw new ArgumentException($"The specified 'configPath': '{configPath}' does not exist.");

        Command command = (clusterName, configPath) switch
        {
            (null, null) => KindCli.WithArguments("create cluster"),
            (null, _) => KindCli.WithArguments($"create cluster --config {configPath}"),
            (_, null) => KindCli.WithArguments($"create cluster --name {clusterName}"),
            _ => KindCli.WithArguments($"create cluster --name {clusterName} --config {configPath}")
        };
        _ = await command.ExecuteBufferedAsync();
    }

    /// <inheritdoc/>
    public async Task<CommandResult> DeleteClusterAsync(string? clusterName = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        Command command = clusterName switch
        {
            null => KindCli.WithArguments("delete cluster"),
            _ => KindCli.WithArguments($"delete cluster --name {clusterName}")
        };
        return await command.ExecuteBufferedAsync();
    }

    /// <inheritdoc/>
    public async Task<CommandResult> ExportLogsAsync(string? clusterName = default, string? logPath = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        Command command = (clusterName, logPath) switch
        {
            (null, null) => KindCli.WithArguments("export logs ./assets/logs/kind-logs"),
            (null, _) => KindCli.WithArguments($"export logs ./assets/logs/{logPath}"),
            (_, null) => KindCli.WithArguments($"export logs ./assets/logs/{clusterName}-logs --name {clusterName}"),
            _ => KindCli.WithArguments($"export logs ./assets/logs/{logPath} --name {clusterName}")
        };
        return await command.ExecuteBufferedAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetClustersAsync()
    {
        Command command = KindCli.WithArguments("get clusters");
        BufferedCommandResult result = await command.ExecuteBufferedAsync();
        List<string> clusters = result.StandardOutput.Split(Environment.NewLine).ToList();
        _ = clusters.RemoveAll(string.IsNullOrWhiteSpace);
        return clusters;
    }

    /// <inheritdoc/>
    public async Task<CommandResult> LoadImageArchiveAsync(string imageArchivePath, string? clusterName = default)
    {
        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");
        if (!File.Exists(imageArchivePath))
            throw new ArgumentException($"The specified 'imageArchivePath': '{imageArchivePath}' does not exist.");
        Command command = clusterName switch
        {
            null => KindCli.WithArguments($"load image-archive {imageArchivePath}"),
            _ => KindCli.WithArguments($"load image-archive {imageArchivePath} --name {clusterName}")
        };
        return await command.ExecuteBufferedAsync();
    }

    /// <inheritdoc/>
    public async Task<CommandResult> LoadImageAsync(Collection<string> imageNames, string? clusterName = default)
    {
        if (imageNames is null || imageNames.Count == 0)
            throw new ArgumentException("The specified 'imageNames' must contain at least one image name.");

        if (clusterName != default && !ClusterNameRegex().Match(clusterName).Success)
            throw new ArgumentException($"The specified 'clusterName': '{clusterName}' is invalid. It must match '^[a-z0-9.-]+$'");

        Command command = clusterName switch
        {
            null => KindCli.WithArguments($"load docker-image {string.Join(' ', imageNames)}"),
            _ => KindCli.WithArguments($"load docker-image {string.Join(' ', imageNames)} --name {clusterName}")
        };
        try
        {
            return await command.ExecuteBufferedAsync();
        }
        catch (CommandExecutionException ex)
        {
            throw new CommandExecutionException(ex.Command, ex.ExitCode, $"Failed to load images: '{string.Join(' ', imageNames)}'", ex.InnerException);
        }
    }
}
