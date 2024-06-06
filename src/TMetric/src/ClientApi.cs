using TMetric.Json;
using V2 = TMetric.Abstractions.V2;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

internal sealed class ClientVersion2Api( HttpClient http ) : V2.IClientApi
{
    public Task<V2.Client[]> Get( int accountId, CancellationToken cancellation = default ) => http.GetFromJsonAsync(
        $"accounts/{accountId}/clients",
        TMetricJsonContext.Default.ClientArray,
        cancellation )!;

    public Task<V2.Client> Get( int accountId, int clientId, CancellationToken cancellation = default ) => http.GetFromJsonAsync(
        $"accounts/{accountId}/clients/{clientId}",
        TMetricJsonContext.Default.Client,
        cancellation )!;
}

internal sealed class ClientVersion3Api( HttpClient http ) : V3.IClientApi
{
    public Task<V3.ClientBasic[]> Get( int accountId, CancellationToken cancellation = default ) => http.GetFromJsonAsync(
        $"v3/accounts/{accountId}/clients",
        TMetricJsonContext.Default.ClientBasicArray,
        cancellation )!;
}