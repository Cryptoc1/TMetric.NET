using TMetric.Abstractions;

namespace TMetric;

public sealed class TMetricClient : ITMetricClient
{
    private readonly HttpClient http;

    public IClientOperations Clients { get; }

    public IInvoiceOperations Invoices { get; }

    public IProjectOperations Projects { get; }

    public TMetricClient(
        IClientOperations clients,
        HttpClient http,
        IInvoiceOperations invoices,
        IProjectOperations projects
    )
    {
        this.http = http;

        Clients = clients;
        Invoices = invoices;
        Projects = projects;
    }

    public Task<string> Version( CancellationToken cancellation )
        => http.GetStringAsync( "version", cancellation );
}