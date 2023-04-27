using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using TMetric.Abstractions;
using V2 = TMetric.Abstractions.V2;

namespace TMetric.Tests;

public sealed class ProjectOperationsTests
{
    [Fact]
    public async Task Gets_projects( )
    {
        using var http = new HttpClient( new ProjectsTestHandler() )
        {
            BaseAddress = new Uri( "https://localhost:8080/" ),
        };

        var operations = new ProjectOperations( http, Options.Create<TMetricOptions>( new() ) );
        var projects = await operations.Get( 0, new() );

        Assert.NotEmpty( projects );
    }

    private sealed class ProjectsTestHandler : HttpMessageHandler
    {
        private static HttpResponseMessage Projects( HttpRequestMessage request ) => new( HttpStatusCode.OK )
        {
            Content = JsonContent.Create( new[] { new V2.ProjectLite(), new V2.ProjectLite() } ),
            RequestMessage = request,
        };

        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            string path = request.RequestUri!.AbsolutePath;
            var response = path switch
            {
                "/accounts/0/projects" => Projects( request ),

                _ => throw new InvalidOperationException( $"Route '{path}' not supported by {GetType().Name}. Add it, or get over it. :)" ),
            };

            return Task.FromResult( response );
        }
    }
}