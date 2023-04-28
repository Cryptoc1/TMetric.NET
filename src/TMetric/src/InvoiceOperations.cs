using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using TMetric.Abstractions;
using TMetric.Abstractions.V2;

namespace TMetric;

public sealed class InvoiceOperations : IInvoiceOperations
{
    private readonly HttpClient http;
    private readonly IOptions<TMetricOptions> optionsAccessor;

    public InvoiceOperations( HttpClient http, IOptions<TMetricOptions> optionsAccessor )
    {
        this.http = http;
        this.optionsAccessor = optionsAccessor;
    }

    /// <inheritdoc/>
    public async Task<Invoice> Create( int accountId, CreateInvoiceParameters parameters, CancellationToken cancellation )
    {
        ArgumentNullException.ThrowIfNull( parameters );
        Validator.ValidateObject( parameters, new( parameters ) );

        var options = optionsAccessor.Value;
        var response = await http.PostAsJsonAsync( $"accounts/{accountId}/invoices", parameters, options.SerializerOptions, cancellation );

        var invoice = await response.Content.ReadFromJsonAsync<Invoice>( options.SerializerOptions, cancellation );
        return invoice!;
    }

    /// <inheritdoc/>
    public async Task<InvoiceExcel> Excel( int accountId, int invoiceId, CancellationToken cancellation )
    {
        var response = await http.GetAsync( $"accounts/{accountId}/invoices/{invoiceId}/xlsx", cancellation );
        _ = response.EnsureSuccessStatusCode();

        return new(
            response.Content.Headers.ContentDisposition!.FileName!,
            await response.Content.ReadAsStreamAsync( cancellation )
        );
    }

    /// <inheritdoc/>
    public Task<Invoice[]> Get( int accountId, GetInvoicesParameters parameters, CancellationToken cancellation )
    {
        ArgumentNullException.ThrowIfNull( parameters );
        Validator.ValidateObject( parameters, new( parameters ) );

        var query = new QueryStringBuilder();
        if( parameters.Clients?.Any() is true )
        {
            foreach( int client in parameters.Clients )
            {
                _ = query.Add( "ClientList", client.ToString() );
            }
        }

        if( parameters.EndDate.HasValue )
        {
            _ = query.Add( "EndDate", parameters.EndDate.Value.ToString( "yyyyMMdd" ) );
        }

        if( parameters.StartDate.HasValue )
        {
            _ = query.Add( "StartDate", parameters.StartDate.Value.ToString( "yyyyMMdd" ) );
        }

        if( parameters.Status.HasValue )
        {
            _ = query.Add( "Status", ( ( int )parameters.Status ).ToString() );
        }

        return http.GetFromJsonAsync<Invoice[]>( $"accounts/{accountId}/invoices{query}", optionsAccessor.Value.SerializerOptions, cancellation )!;
    }

    /// <inheritdoc/>
    public Task<Invoice> Get( int accountId, int invoiceId, CancellationToken cancellation )
        => http.GetFromJsonAsync<Invoice>( $"accounts/{accountId}/invoices/{invoiceId}", optionsAccessor.Value.SerializerOptions, cancellation )!;

    /// <inheritdoc/>
    public async Task Put( int accountId, Invoice invoice, CancellationToken cancellation )
    {
        ArgumentNullException.ThrowIfNull( invoice );
        Validator.ValidateObject( invoice, new( invoice ) );

        using var response = await http.PutAsJsonAsync(
            $"accounts/{accountId}/invoices/{invoice.InvoiceId}",
            invoice,
            optionsAccessor.Value.SerializerOptions,
            cancellation
        );

        _ = response.EnsureSuccessStatusCode();
    }
}