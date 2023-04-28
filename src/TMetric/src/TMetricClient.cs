using TMetric.Abstractions;
using V2 = TMetric.Abstractions.V2;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

public sealed class TMetricClient : ITMetricClient
{
    public V2.IApiOperations V2 { get; }

    public V3.IApiOperations V3 { get; }

    public TMetricClient( V2.IApiOperations v2, V3.IApiOperations v3 )
        => (V2, V3) = (v2, v3);
}

public sealed class Version2Operations : V2.IApiOperations
{
    private readonly HttpClient http;

    public V2.IClientOperations Clients { get; }

    public V2.IInvoiceOperations Invoices { get; }

    public V2.IProjectOperations Projects { get; }

    public Version2Operations(
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

public sealed class Version3Operations : V3.IApiOperations
{
    public V3.IClientOperations Clients { get; }

    public V3.ITimeEntryOperations TimeEntries { get; }

    public Version3Operations( HttpClient _, V3.IClientOperations clients, V3.ITimeEntryOperations timeEntries )
        => (Clients, TimeEntries) = (clients, timeEntries);
}