using TMetric.Abstractions;
using V2 = TMetric.Abstractions.V2;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

public sealed class TMetricClient : ITMetricClient
{
    public IVersion2Api V2 { get; }

    public IVersion3Api V3 { get; }

    public TMetricClient( IVersion2Api v2, IVersion3Api v3 )
        => (V2, V3) = (v2, v3);
}

public sealed class Version2Api : IVersion2Api
{
    private readonly HttpClient http;

    public V2.IClientOperations Clients { get; }

    public V2.IInvoiceOperations Invoices { get; }

    public V2.IProjectOperations Projects { get; }

    public Version2Api(
        HttpClient http,
        V2.IClientOperations clients,
        V2.IInvoiceOperations invoices,
        V2.IProjectOperations projects
    )
    {
        this.http = http;

        Clients = clients;
        Invoices = invoices;
        Projects = projects;
    }

    public Task<string> Version( CancellationToken cancellation = default )
        => http.GetStringAsync( "version", cancellation );
}

public sealed class Version3Api : IVersion3Api
{
    public V3.IClientOperations Clients { get; }

    public Version3Api( V3.IClientOperations clients )
        => Clients = clients;
}