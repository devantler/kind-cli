using CliWrap;
using CliWrap.Buffered;

namespace Devantler.KindCLI;

public class KindCliService : IKindCliService
{
    public Command KindCli => Cli.Wrap("kind");

    public async Task CreateClusterAsync(string? clusterName = default, string? configPath = default)
    {
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
        var command = clusterName switch
        {
            null => KindCli.WithArguments("delete cluster"),
            _ => KindCli.WithArguments($"delete cluster --name {clusterName}")
        };
        return await command.ExecuteBufferedAsync();
    }

    public async Task<CommandResult> ExportLogsAsync(string? clusterName = default, string? logPath = default)
    {
        var command = (clusterName, logPath) switch
        {
            (null, null) => KindCli.WithArguments("export logs"),
            (null, _) => KindCli.WithArguments($"export logs {logPath}"),
            (_, null) => KindCli.WithArguments($"export logs --name {clusterName}"),
            _ => KindCli.WithArguments($"export logs {logPath} --name {clusterName}")
        };
        return await command.ExecuteAsync();
    }

    public async Task<IEnumerable<string>> GetClustersAsync()
    {
        var command = KindCli.WithArguments("get clusters");
        var result = await command.ExecuteBufferedAsync();
        var clusters = result.StandardOutput.Split(Environment.NewLine).ToList();
        clusters.RemoveAll(x => string.IsNullOrEmpty(x));
        return clusters;
    }

    public async Task<CommandResult> LoadImageArchiveAsync(string imageArchivePath, string? clusterName = default)
    {
        var command = clusterName switch
        {
            null => KindCli.WithArguments($"load image-archive {imageArchivePath}"),
            _ => KindCli.WithArguments($"load image-archive {imageArchivePath} --name {clusterName}")
        };
        return await command.ExecuteAsync();
    }

    public async Task<CommandResult> LoadImageAsync(List<string> imageNames, string? clusterName = default)
    {
        var command = clusterName switch
        {
            null => KindCli.WithArguments($"load docker-image {string.Join(' ', imageNames)}"),
            _ => KindCli.WithArguments($"load docker-image {string.Join(' ', imageNames)} --name {clusterName}")
        };
        return await command.ExecuteAsync();
    }
}