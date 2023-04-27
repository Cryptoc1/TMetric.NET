using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using TMetric.Abstractions;

using V2 = TMetric.Abstractions.V2;
using V3 = TMetric.Abstractions.V3;

namespace TMetric.Tests;

public sealed class ClientOperationsTests
{
    [Fact]
    public async Task Gets_clients( )
    {
        using var http = new HttpClient( new ClientsTestHandler() )
        {
            BaseAddress = new Uri( "https://localhost:8080/" ),
        };

        var operations = new ClientV2Operations( http, Options.Create<TMetricOptions>( new() ) );
        var clients = await operations.Get( 0 );

        Assert.NotEmpty( clients );
    }

    [Fact]
    public async Task Gets_clients_v3( )
    {
        using var http = new HttpClient( new ClientsTestHandler() )
        {
            BaseAddress = new Uri( "https://localhost:8080/" ),
        };

        var options = Options.Create<TMetricOptions>( new() );
        var v3 = new ClientV3Operations( http, options );

        var operations = new ClientV3Operations( http, options );
        var clients = await operations.Get( 0 );

        Assert.NotEmpty( clients );
    }

    [Fact]
    public async Task Gets_client( )
    {
        using var http = new HttpClient( new ClientsTestHandler() )
        {
            BaseAddress = new Uri( "https://localhost:8080/" ),
        };

        var operations = new ClientV2Operations( http, Options.Create<TMetricOptions>( new() ) );
        var client = await operations.Get( 0, 0 );

        Assert.NotNull( client );
    }

    private sealed class ClientsTestHandler : HttpMessageHandler
    {
        private static HttpResponseMessage Client( HttpRequestMessage request ) => new( HttpStatusCode.OK )
        {
            Content = JsonContent.Create( new V2.Client() ),
            RequestMessage = request,
        };

        private static HttpResponseMessage Clients( HttpRequestMessage request ) => new( HttpStatusCode.OK )
        {
            Content = JsonContent.Create( new[] { new V2.Client(), new V2.Client() } ),
            RequestMessage = request,
        };

        private static HttpResponseMessage ClientsV3( HttpRequestMessage request ) => new( HttpStatusCode.OK )
        {
            Content = JsonContent.Create( new[] { new V3.Client(), new V3.Client() } ),
            RequestMessage = request,
        };

        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            string path = request.RequestUri!.AbsolutePath;
            var response = path switch
            {
                "/accounts/0/clients" => Clients( request ),
                "/accounts/0/clients/0" => Client( request ),
                "/v3/accounts/0/clients" => ClientsV3( request ),

                _ => throw new InvalidOperationException( $"Route '{path}' not supported by {GetType().Name}. Add it, or get over it. :)" ),
            };

            return Task.FromResult( response );
        }
    }
}