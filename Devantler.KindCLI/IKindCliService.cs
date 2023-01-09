using System.Collections.ObjectModel;
using CliWrap;

namespace Devantler.KindCLI;

/// <summary>
/// An interface for a service that provides access to the Kind CLI.
/// </summary>
public interface IKindCliService
{
    /// <summary>
    /// Gets a Kind CLI wrapper for Kind CLI commands.
    /// </summary>
    public Command KindCli { get; }

    /// <summary>
    /// Gets a list of Kind clusters.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<string>> GetClustersAsync();

    /// <summary>
    /// Creates a Kind cluster.
    /// </summary>
    /// <param name="clusterName"></param>
    /// <param name="configPath"></param>
    /// <returns></returns>
    Task CreateClusterAsync(string? clusterName = default, string? configPath = default);

    /// <summary>
    /// Deletes a Kind cluster.
    /// </summary>
    /// <param name="clusterName"></param>
    /// <returns></returns>
    Task<CommandResult> DeleteClusterAsync(string? clusterName = default);

    /// <summary>
    /// Loads Docker images into a Kind cluster.
    /// </summary>
    /// <param name="imageNames"></param>
    /// <param name="clusterName"></param>
    /// <returns></returns>
    Task<CommandResult> LoadImageAsync(Collection<string> imageNames, string? clusterName = default);

    /// <summary>
    /// Loads Docker images from an image archive into a Kind cluster.
    /// </summary>
    /// <param name="imageArchivePath"></param>
    /// <param name="clusterName"></param>
    /// <returns></returns>
    Task<CommandResult> LoadImageArchiveAsync(string imageArchivePath, string? clusterName = default);

    /// <summary>
    /// Exports logs from a Kind cluster.
    /// </summary>
    /// <param name="clusterName"></param>
    /// <param name="logPath"></param>
    /// <returns></returns>
    Task<CommandResult> ExportLogsAsync(string? clusterName = default, string? logPath = default);
}
