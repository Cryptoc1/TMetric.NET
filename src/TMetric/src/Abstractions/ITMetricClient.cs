namespace TMetric.Abstractions;

public interface ITMetricClient
{
    IClientOperations Clients { get; }

    IInvoiceOperations Invoices { get; }

    Task<string> Version( CancellationToken cancellation = default );
}