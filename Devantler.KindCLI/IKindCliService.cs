using CliWrap;
using CliWrap.Buffered;

namespace Devantler.KindCLI;

public interface IKindCliService
{
    public Command KindCli { get; }
    Task<IEnumerable<string>> GetClustersAsync();
    Task CreateClusterAsync(string? clusterName = default, string? configPath = default);
    Task<CommandResult> DeleteClusterAsync(string? clusterName = default);
    Task<CommandResult> LoadImageAsync(List<string> imageNames, string? clusterName = default);
    Task<CommandResult> LoadImageArchiveAsync(string imageArchivePath, string? clusterName = default);
    Task<CommandResult> ExportLogsAsync(string? clusterName = default, string? logPath = default);
}