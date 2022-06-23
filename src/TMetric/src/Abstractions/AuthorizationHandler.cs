namespace TMetric.Abstractions;

/// <summary> Describes a <see cref="DelegatingHandler"/> that can provide a TMetric API Key to be used for the authorization of requests. </summary>
public abstract class AuthorizationHandler : DelegatingHandler
{
    /// <summary> Retrieve the TMetric API Key to be used for authorization. </summary>
    protected abstract ValueTask<string> GetApiKey( CancellationToken cancellation );

    protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellation )
    {
        if( request.Headers.Authorization is null )
        {
            string key = await GetApiKey( cancellation );
            if( !string.IsNullOrWhiteSpace( key ) )
            {
                request.Headers.Authorization = new( "Bearer", key );
            }
        }

        return await base.SendAsync( request, cancellation );
    }
}