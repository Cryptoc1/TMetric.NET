using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric.Tests;

public sealed class ClientOperationsTests
{
    [Fact]
    public async Task Operations_gets_clients( )
    {
        using var http = new HttpClient( new ClientsTestHandler() )
        {
            BaseAddress = new Uri( "https://localhost:8080/" ),
        };

        var operations = new ClientOperations( http, Options.Create<TMetricOptions>( new() ) );
        var clients = await operations.Get( 0 );

        Assert.NotEmpty( clients );
    }

    [Fact]
    public async Task Operations_gets_client( )
    {
        using var http = new HttpClient( new ClientsTestHandler() )
        {
            BaseAddress = new Uri( "https://localhost:8080/" ),
        };

        var operations = new ClientOperations( http, Options.Create<TMetricOptions>( new() ) );
        var client = await operations.Get( 0, 0 );

        Assert.NotNull( client );
    }

    private sealed class ClientsTestHandler : HttpMessageHandler
    {
        private static HttpResponseMessage Client( HttpRequestMessage request ) => new( HttpStatusCode.OK )
        {
            Content = JsonContent.Create( new Client() ),
            RequestMessage = request,
        };

        private static HttpResponseMessage Clients( HttpRequestMessage request ) => new( HttpStatusCode.OK )
        {
            Content = JsonContent.Create( new[] { new Client(), new Client() } ),
            RequestMessage = request,
        };

        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            string path = request.RequestUri!.AbsolutePath;
            var response = path switch
            {
                "accounts/0/clients" => Clients( request ),
                "accounts/0/clients/0" => Client( request ),

                _ => throw new InvalidOperationException( $"Route not supported by {GetType().Name}. Add it, or get over it. :)" ),
            };

            return Task.FromResult( response );
        }
    }
}