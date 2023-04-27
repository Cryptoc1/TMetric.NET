namespace TMetric.Abstractions.V2;

public interface IApiOperations
{
    IClientOperations Clients { get; }

    IInvoiceOperations Invoices { get; }

    IProjectOperations Projects { get; }

    Task<string> Version( CancellationToken cancellation = default );
}