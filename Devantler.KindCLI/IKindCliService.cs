using CliWrap;
using CliWrap.Buffered;

namespace Devantler.KindCLI;

public interface IKindCliService
{
    public Command KindCli { get; }
    Task<IEnumerable<string>> GetClustersAsync();
    Task<BufferedCommandResult> GetClusterInfoAsync(string clusterName);
    Task CreateClusterAsync(string? clusterName = default, string? configPath = default);
    Task<CommandResult> DeleteClusterAsync(string? clusterName);
    Task<CommandResult> LoadImageAsync(List<string> imageNames, string? clusterName);
    Task<CommandResult> LoadImageArchiveAsync(List<string> imageNames, string? clusterName);
    Task<CommandResult> ExportLogsAsync(string? clusterName, string? logPath);
}