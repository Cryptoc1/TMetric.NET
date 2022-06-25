using TMetric.Abstractions;

namespace TMetric.Extensions;

public static class InvoiceOperationsExtensions
{
    public static async Task<Invoice?> GetLatest( this IInvoiceOperations invoiceOperations, int accountId, int clientId, CancellationToken cancellation = default )
    {
        ArgumentNullException.ThrowIfNull( invoiceOperations );
        var parameters = new GetInvoicesParameters
        {
            Clients = new[] { clientId },
        };

        var invoices = await invoiceOperations.Get( accountId, parameters, cancellation );
        if( invoices?.Any() is true )
        {
            cancellation.ThrowIfCancellationRequested();

            var invoice = invoices.OrderByDescending( invoice => invoice.IssueDate ).First();
            return await invoiceOperations.Get( accountId, invoice.InvoiceId, cancellation );
        }

        return default;
    }
}