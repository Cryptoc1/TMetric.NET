namespace TMetric.Abstractions;

public interface ITMetricClient
{
    IClientOperations Clients { get; }

    IInvoiceOperations Invoices { get; }

    IProjectOperations Projects { get; }

    Task<string> Version( CancellationToken cancellation = default );
}