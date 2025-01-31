namespace TMetric.Abstractions.V2;

public interface IVersion2Api
{
    public IClientApi Clients { get; }
    public IInvoiceApi Invoices { get; }
    public IProjectApi Projects { get; }

    public Task<string> Version( CancellationToken cancellation = default );
}