<h1 align="center">TMetric.NET</h1>

<div align="center">

*Unofficial .NET wrapper of the TMetric REST APIs.*

![Language](https://img.shields.io/github/languages/top/cryptoc1/tmetric.net)
![Dependencies](https://img.shields.io/librariesio/github/cryptoc1/tmetric.net)
[![Checks](https://img.shields.io/github/checks-status/cryptoc1/tmetric.net/develop)](https://github.com/Cryptoc1/tmetric.net/actions/workflows/default.yml)
[![Coverage](https://img.shields.io/codecov/c/github/cryptoc1/tmetric.net)](https://app.codecov.io/gh/Cryptoc1/tmetric.net/)
[![Version](https://img.shields.io/nuget/vpre/TMetric.NET)](https://www.nuget.org/packages/TMetric.NET)

</div>

## Basic Usage

```csharp
var services = new ServiceCollection()
    .AddTMetric( options => options.ApiKey = "..." )
    .BuildServiceProvider();

var tmetric = services.GetService<ITMetricClient>();

int clientId = ...;
var client = await tmetric.Clients.Get( clientId );

Console.WriteLine( $"Client: {client.ClientName} {client.ClientId}" );
```