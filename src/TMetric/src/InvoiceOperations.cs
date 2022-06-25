using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric;

public sealed class InvoiceOperations : IInvoiceOperations
{
    public readonly HttpClient http;
    private readonly IOptions<TMetricOptions> optionsAccessor;

    public InvoiceOperations( HttpClient http, IOptions<TMetricOptions> optionsAccessor )
    {
        this.http = http;
        this.optionsAccessor = optionsAccessor;
    }

    public async Task<Invoice> Create( int accountId, CreateInvoiceParameters parameters, CancellationToken cancellation = default )
    {
        ArgumentNullException.ThrowIfNull( parameters );
        Validator.ValidateObject( parameters, new( parameters ) );

        var options = optionsAccessor.Value;

        var response = await http.PostAsJsonAsync( $"accounts/{accountId}/invoices", parameters, options.SerializerOptions, cancellation );

        var content = await response.Content.ReadAsStringAsync(cancellation);

        var invoice = await response.Content.ReadFromJsonAsync<Invoice>( options.SerializerOptions, cancellation );
        return invoice!;
    }

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

        if( parameters.EndDate is not null )
        {
            _ = query.Add( "EndDate", parameters.EndDate.Value.ToString( "yyyyMMdd" ) );
        }

        if( parameters.StartDate is not null )
        {
            _ = query.Add( "StartDate", parameters.StartDate.Value.ToString( "yyyyMMdd" ) );
        }

        if( parameters.Status is not null )
        {
            _ = query.Add( "Status", ( ( int )parameters.Status ).ToString() );
        }

        return http.GetFromJsonAsync<Invoice[]>( $"accounts/{accountId}/invoices{query}", optionsAccessor.Value.SerializerOptions, cancellation )!;
    }

    public Task<Invoice> Get( int accountId, int invoiceId, CancellationToken cancellation )
        => http.GetFromJsonAsync<Invoice>( $"accounts/{accountId}/invoices/{invoiceId}", optionsAccessor.Value.SerializerOptions, cancellation )!;
}