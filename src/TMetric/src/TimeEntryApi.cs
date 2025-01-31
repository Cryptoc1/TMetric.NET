using System.Globalization;
using ESCd.Extensions.Http;
using TMetric.Json;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

internal sealed class TimeEntryApi( HttpClient http ) : V3.ITimeEntryApi
{
    public Task<V3.TimeEntry[]> Get( int accountId, V3.GetTimeEntriesParameters parameters, CancellationToken cancellation = default )
    {
        ArgumentOutOfRangeException.ThrowIfLessThan( accountId, 0 );
        ArgumentNullException.ThrowIfNull( parameters );

        var query = new QueryStringBuilder()
            .Append( "userId", parameters.UserId );

        if( parameters.EndDate.HasValue )
        {
            query.Append( "endDate", parameters.EndDate.Value.ToString( "yyyy-MM-dd", CultureInfo.InvariantCulture ) );
        }

        if( parameters.StartDate.HasValue )
        {
            query.Append( "startDate", parameters.StartDate.Value.ToString( "yyyy-MM-dd", CultureInfo.InvariantCulture ) );
        }

        return http.GetFromJsonAsync(
            $"v3/accounts/{accountId}/timeentries{query}",
            TMetricJsonContext.Default.TimeEntryArray,
            cancellation )!;
    }
}