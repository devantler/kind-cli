# KindCLI

A dotnet library to run Kind CLI commands.

## Requirements

- .NET 6.0
- Docker

The host must be either:

- linux-arm64
- linux-amd64
- linux-s390x
- windows-amd64
- darwin-arm64 (macos)
- darwin-amd64 (macos)

## How to use

Register the KindCLI service in your DI container:

```csharp
services.AddKindCliService();
```

Then inject the service where you need it:

```csharp
private readonly IKindCliService _kindCliService;

public ClassName(KindCliService kindCliService) {
    _kindCliService = kindCliService;
}

```

Call the different methods on the `KindCliService` to run commands:

```csharp
_kindCliService.GetClustersAsync(); // Returns a list of all clusters
_kindCliService.GetClusterInfoAsync(...); // Gets info about a specific cluster
_kindCliService.CreateClusterAsync(...); // Creates a new cluster
_kindCliService.DeleteClusterAsync(...); // Deletes a cluster
_kindCliService.LoadImageAsync(...); // Loads an image into a cluster
_kindCliService.LoadImageArchiveAsync(...); // Loads an image archive into a cluster
_kindCliService.ExportLogsAsync(...); // Exports logs from a cluster
```

delete me later
