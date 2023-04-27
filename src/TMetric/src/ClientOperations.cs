using Microsoft.Extensions.Options;

using TMetric.Abstractions;
using V2 = TMetric.Abstractions.V2;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

public sealed class ClientV2Operations : BaseApiOperations, V2.IClientOperations
{
    public ClientV2Operations( HttpClient http, IOptions<TMetricOptions> options )
        : base( http, options )
    {
    }

    public Task<V2.Client[]> Get( int accountId, CancellationToken cancellation = default )
        => Http.GetFromJsonAsync<V2.Client[]>( $"accounts/{accountId}/clients", Options.Value.SerializerOptions, cancellation )!;

    public Task<V2.Client> Get( int accountId, int clientId, CancellationToken cancellation = default )
        => Http.GetFromJsonAsync<V2.Client>( $"accounts/{accountId}/clients/{clientId}", Options.Value.SerializerOptions, cancellation )!;
}

public sealed class ClientV3Operations : BaseApiOperations, V3.IClientOperations
{
    public ClientV3Operations( HttpClient http, IOptions<TMetricOptions> options )
        : base( http, options )
    {
    }

    public Task<V3.ClientBasic[]> Get( int accountId, CancellationToken cancellation = default )
        => Http.GetFromJsonAsync<V3.ClientBasic[]>( $"v3/accounts/{accountId}/clients", Options.Value.SerializerOptions, cancellation )!;
}