using TMetric.Abstractions;

namespace TMetric;

public sealed class TMetricClient : ITMetricClient
{
    private readonly HttpClient http;

    public IClientOperations Clients { get; }

    public IInvoiceOperations Invoices { get; }

    public TMetricClient(
        IClientOperations clients,
        HttpClient http,
        IInvoiceOperations invoices
    )
    {
        this.http = http;

        Clients = clients;
        Invoices = invoices;
    }

    public Task<string> Version( CancellationToken cancellation = default )
        => http.GetStringAsync( "version" );
}