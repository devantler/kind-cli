# KindCLI

[![codecov](https://codecov.io/github/devantler/kind-cli/branch/main/graph/badge.svg?token=4TRICYGDC1)](https://codecov.io/github/devantler/kind-cli)

A dotnet library to run Kind CLI commands. It is a wrapper around the Kind CLI and is pre-packaged with the needed binaries.

## Requirements

- .NET 6.0
- Docker (it must be running on the host machine)

The host must be either:

- Linux Arm64
- Linux Amd64
- Linux s390x
- Windows Amd64
- Darwin Arm64
- Darwin Amd64

In other words, it works on Linux, Windows and MacOS, with 64bit AMD and Arm CPU Architectures.

## How to use

1. Register the KindCLI service in your DI container:

    ```csharp
    services.AddKindCliService();
    ```

2. Inject the service where you need it:

    ```csharp
    public ClassName {
        private readonly IKindCliService _kindCliService;

        public ClassName(KindCliService kindCliService) {
            _kindCliService = kindCliService;
        }
    }
    ```

3. Call the different methods on the `KindCliService` to run commands:

    ```csharp
    _kindCliService.GetClustersAsync(); // Returns a list of all clusters
    _kindCliService.CreateClusterAsync(...); // Creates a new cluster
    _kindCliService.DeleteClusterAsync(...); // Deletes a cluster
    _kindCliService.LoadImageAsync(...); // Loads an image into a cluster
    _kindCliService.LoadImageArchiveAsync(...); // Loads an image archive into a cluster
    _kindCliService.ExportLogsAsync(...); // Exports logs from a cluster
    ```
