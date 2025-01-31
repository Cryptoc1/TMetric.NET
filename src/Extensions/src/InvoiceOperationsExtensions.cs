using TMetric.Abstractions.V2;

namespace TMetric.Extensions;

/// <summary> Extension for <see cref="IInvoiceApi"/> </summary>
public static class InvoiceOperationsExtensions
{
    /// <summary> Retrieve the latest invoice, if one exists. </summary>
    public static async Task<Invoice?> GetLatest( this IInvoiceApi invoiceOperations, int accountId, int clientId, CancellationToken cancellation = default )
    {
        ArgumentNullException.ThrowIfNull( invoiceOperations );
        var parameters = new GetInvoicesParameters
        {
            Clients = [ clientId ],
        };

        var invoices = await invoiceOperations.Get( accountId, parameters, cancellation );
        if( invoices?.Length is not (null or 0) )
        {
            var invoice = invoices.OrderByDescending( invoice => invoice.IssueDate ).First();
            return await invoiceOperations.Get( accountId, invoice.InvoiceId, cancellation );
        }

        return default;
    }
}