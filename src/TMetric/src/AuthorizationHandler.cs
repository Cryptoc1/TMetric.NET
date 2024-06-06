using TMetric.Abstractions;

namespace TMetric;

internal sealed class AuthorizationHandler( ApiCredential credential ) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellation )
    {
        if( request.Headers.Authorization is null )
        {
            var key = await credential.Acquire( cancellation );
            if( !string.IsNullOrWhiteSpace( key ) )
            {
                request.Headers.Authorization = new( "Bearer", key );
            }
        }

        return await base.SendAsync( request, cancellation );
    }

}