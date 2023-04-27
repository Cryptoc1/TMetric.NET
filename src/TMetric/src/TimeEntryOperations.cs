using Microsoft.Extensions.Options;
using TMetric.Abstractions;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

public sealed class TimeEntryV3Operations : BaseApiOperations, V3.ITimeEntryOperations
{
    public TimeEntryV3Operations( HttpClient http, IOptions<TMetricOptions> options )
        : base( http, options )
    {
    }

    public Task<V3.TimeEntry[]> Get( int accountId, V3.GetTimeEntriesParameters parameters, CancellationToken cancellation = default )
    {
        ArgumentNullException.ThrowIfNull( parameters );

        var query = new QueryStringBuilder();
        if( parameters.UserId.HasValue )
        {
            _ = query.Add( "userId", parameters.UserId.Value.ToString() );
        }

        if( parameters.EndDate.HasValue )
        {
            _ = query.Add( "EndDate", parameters.EndDate.Value.ToQueryString() );
        }

        if( parameters.StartDate.HasValue )
        {
            _ = query.Add( "StartDate", parameters.StartDate.Value.ToQueryString() );
        }

        return Http.GetFromJsonAsync<V3.TimeEntry[]>( $"v3/accounts/{accountId}/timeentries{query}", Options.Value.SerializerOptions, cancellation )!;
    }
}