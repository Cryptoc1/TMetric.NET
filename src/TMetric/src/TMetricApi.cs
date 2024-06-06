using TMetric.Abstractions;
using V2 = TMetric.Abstractions.V2;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

internal sealed class TMetricApi( HttpClient http ) : ITMetricApi
{
    public V2.IVersion2Api V2 { get; } = new Version2Api( http );

    public V3.IVersion3Api V3 { get; } = new Version3Api( http );
}

internal sealed class Version2Api( HttpClient http ) : V2.IVersion2Api
{
    public V2.IClientApi Clients { get; } = new ClientVersion2Api( http );

    public V2.IInvoiceApi Invoices { get; } = new InvoiceApi( http );

    public V2.IProjectApi Projects { get; } = new ProjectApi( http );

    public Task<string> Version( CancellationToken cancellation = default )
        => http.GetStringAsync( "version", cancellation );
}

internal sealed class Version3Api( HttpClient http ) : V3.IVersion3Api
{
    public V3.IClientApi Clients { get; } = new ClientVersion3Api( http );

    public V3.ITimeEntryApi TimeEntries { get; } = new TimeEntryApi( http );
}