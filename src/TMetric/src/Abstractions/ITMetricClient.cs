namespace TMetric.Abstractions;

public interface ITMetricClient
{
    IVersion2Api V2 { get; }

    IVersion3Api V3 { get; }
}

public interface IVersion2Api
{
    V2.IClientOperations Clients { get; }

    V2.IInvoiceOperations Invoices { get; }

    V2.IProjectOperations Projects { get; }

    Task<string> Version( CancellationToken cancellation = default );
}

public interface IVersion3Api
{
    V3.IClientOperations Clients { get; }

    //IInvoiceOperations Invoices { get; }

    //IProjectOperations Projects { get; }
}