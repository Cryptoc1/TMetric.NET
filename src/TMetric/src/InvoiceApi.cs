using System.Globalization;
using TMetric.Abstractions.V2;
using TMetric.Json;

namespace TMetric;

internal sealed class InvoiceApi( HttpClient http ) : IInvoiceApi
{
    public async Task<Invoice> Create( int accountId, CreateInvoiceParameters parameters, CancellationToken cancellation )
    {
        Validation.ThrowIfInvalid( parameters );

        using var response = await http.PostAsJsonAsync(
            $"accounts/{accountId}/invoices",
            parameters,
            TMetricJsonContext.Default.CreateInvoiceParameters,
            cancellation );

        var invoice = await response.EnsureSuccessStatusCode()
            .Content
            .ReadFromJsonAsync( TMetricJsonContext.Default.Invoice, cancellation );

        return invoice!;
    }

    public async Task Delete( int accountId, int invoiceId, CancellationToken cancellation )
    {
        using var response = await http.DeleteAsync( $"accounts/{accountId}/invoices/{invoiceId}", cancellation );
        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc/>
    public async Task<InvoiceExcel> Excel( int accountId, int invoiceId, CancellationToken cancellation )
    {
        using var response = await http.GetAsync( $"accounts/{accountId}/invoices/{invoiceId}/xlsx", cancellation );
        response.EnsureSuccessStatusCode();

        return new(
            response.Content.Headers.ContentDisposition!.FileName!,
            await response.Content.ReadAsStreamAsync( cancellation ) );
    }

    /// <inheritdoc/>
    public Task<Invoice[]> Get( int accountId, GetInvoicesParameters parameters, CancellationToken cancellation )
    {
        ArgumentNullException.ThrowIfNull( parameters );

        var query = new QueryStringBuilder();
        if( parameters.Clients?.Count is not (null or 0) )
        {
            foreach( var client in parameters.Clients )
            {
                query.Add( "ClientList", client );
            }
        }

        if( parameters.EndDate.HasValue )
        {
            query.Add( "EndDate", parameters.EndDate.Value.ToString( "yyyyMMdd", CultureInfo.InvariantCulture ) );
        }

        if( parameters.StartDate.HasValue )
        {
            query.Add( "StartDate", parameters.StartDate.Value.ToString( "yyyyMMdd", CultureInfo.InvariantCulture ) );
        }

        if( parameters.Status.HasValue )
        {
            query.Add( "Status", ( int )parameters.Status );
        }

        return http.GetFromJsonAsync(
            $"accounts/{accountId}/invoices{query}",
            TMetricJsonContext.Default.InvoiceArray,
            cancellation )!;
    }

    /// <inheritdoc/>
    public Task<Invoice> Get( int accountId, int invoiceId, CancellationToken cancellation ) => http.GetFromJsonAsync(
        $"accounts/{accountId}/invoices/{invoiceId}",
        TMetricJsonContext.Default.Invoice,
        cancellation )!;

    /// <inheritdoc/>
    public async Task Put( int accountId, Invoice invoice, CancellationToken cancellation )
    {
        Validation.ThrowIfInvalid( invoice );

        using var response = await http.PutAsJsonAsync(
            $"accounts/{accountId}/invoices/{invoice.InvoiceId}",
            invoice,
            TMetricJsonContext.Default.Invoice,
            cancellation );

        response.EnsureSuccessStatusCode();
    }
}