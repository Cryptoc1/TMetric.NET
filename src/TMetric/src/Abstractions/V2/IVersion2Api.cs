namespace TMetric.Abstractions.V2;

public interface IVersion2Api
{
    IClientApi Clients { get; }

    IInvoiceApi Invoices { get; }

    IProjectApi Projects { get; }

    Task<string> Version( CancellationToken cancellation = default );
}