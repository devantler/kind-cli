using CliWrap;
using CliWrap.Buffered;

namespace Devantler.KindCLI;

public class KindCliService : IKindCliService
{
    public Command KindCli => Cli.Wrap("kind");

    public async Task CreateClusterAsync(string? clusterName, string? configPath)
    {
        var command = (clusterName, configPath) switch
        {
            (null, null) => KindCli.WithArguments("create cluster"),
            (null, _) => KindCli.WithArguments($"create cluster --config {configPath}"),
            (_, null) => KindCli.WithArguments($"create cluster --name {clusterName}"),
            _ => KindCli.WithArguments($"create cluster --name {clusterName} --config {configPath}")
        };
        await command.ExecuteAsync();
    }

    public async Task<CommandResult> DeleteClusterAsync(string? clusterName)
    {
        var command = clusterName switch
        {
            null => KindCli.WithArguments("delete cluster"),
            _ => KindCli.WithArguments($"delete cluster --name {clusterName}")
        };
        return await command.ExecuteAsync();
    }

    public async Task<CommandResult> ExportLogsAsync(string? clusterName, string? logPath)
    {
        throw new NotImplementedException();
    }

    public async Task<BufferedCommandResult> GetClusterInfoAsync(string? clusterName)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> GetClustersAsync()
    {
        var command = KindCli.WithArguments("get clusters");
        var result = await command.ExecuteBufferedAsync();
        var clusters = result.StandardOutput.Split(Environment.NewLine).ToList();
        clusters.RemoveAll(x => string.IsNullOrEmpty(x));
        return clusters;
    }

    public async Task<CommandResult> LoadImageArchiveAsync(string? clusterName, string imageArchivePath)
    {
        throw new NotImplementedException();
    }

    public async Task<CommandResult> LoadImageAsync(string? clusterName, List<string> imageNames)
    {
        throw new NotImplementedException();
    }
}