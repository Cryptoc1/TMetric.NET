using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric;

public sealed class ClientOperations : IClientOperations
{
    public readonly HttpClient http;
    private readonly IOptions<TMetricOptions> optionsAccessor;

    public ClientOperations( HttpClient http, IOptions<TMetricOptions> optionsAccessor )
    {
        this.http = http;
        this.optionsAccessor = optionsAccessor;
    }

    public Task<Client[]> Get( int accountId, CancellationToken cancellation = default )
        => http.GetFromJsonAsync<Client[]>( $"accounts/{accountId}/clients", optionsAccessor.Value.SerializerOptions, cancellation )!;

    public Task<Client> Get( int accountId, int clientId, CancellationToken cancellation = default )
        => http.GetFromJsonAsync<Client>( $"accounts/{accountId}/clients/{clientId}", optionsAccessor.Value.SerializerOptions, cancellation )!;
}