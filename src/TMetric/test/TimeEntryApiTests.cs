using System.Net;
using System.Net.Http.Json;
using TMetric.Json;
using V3 = TMetric.Abstractions.V3;

namespace TMetric.Tests;

public sealed class TimeEntryApiTests
{
    [Fact]
    public async Task Gets_timeentries_v3( )
    {
        using var http = new HttpClient( new TimeEntryTestHandler() )
        {
            BaseAddress = new Uri( "https://localhost:8080/" ),
        };

        var api = new TimeEntryApi( http );
        var entries = await api.Get( 0, new() );

        Assert.NotEmpty( entries );
    }

    private sealed class TimeEntryTestHandler : HttpMessageHandler
    {
        private static HttpResponseMessage TimeEntriesV3( HttpRequestMessage request ) => new( HttpStatusCode.OK )
        {
            Content = JsonContent.Create( [ new V3.TimeEntry(), new V3.TimeEntry() ], TMetricJsonContext.Default.TimeEntryArray ),
            RequestMessage = request,
        };

        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            var path = request.RequestUri!.AbsolutePath;
            var response = path switch
            {
                "/v3/accounts/0/timeentries" => TimeEntriesV3( request ),
                _ => throw new NotSupportedException( $"Route '{path}' not supported by {GetType().Name}. Add it, or get over it. :)" ),
            };

            return Task.FromResult( response );
        }
    }
}