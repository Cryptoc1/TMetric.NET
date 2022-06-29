using System.Net;
using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric.Tests;

public sealed class DefaultAuthorizationHandlerTests
{
    [Fact]
    public async Task Handler_adds_bearer_authorization_header( )
    {
        var options = Options.Create<TMetricOptions>( new() { ApiKey = "Hello, World!" } );
        using var handler = new DefaultAuthorizationHandler( options )
        {
            InnerHandler = new TestResponseHandler(),
        };

        using var invoker = new HttpMessageInvoker( handler );

        using var request = new HttpRequestMessage( HttpMethod.Get, new Uri( "https://localhost:8080" ) );
        using var response = await invoker.SendAsync( request, CancellationToken.None );

        var authorization = response.RequestMessage!.Headers.Authorization;

        Assert.NotNull( authorization );
        Assert.Equal( "Bearer", authorization!.Scheme );
        Assert.Equal( options.Value.ApiKey, authorization!.Parameter );
    }

    [Theory]
    [InlineData( "" )]
    [InlineData( null )]
    [InlineData( " " )]
    public async Task Handler_does_not_add_empty_bearer_authorization_header( string apiKey )
    {
        var options = Options.Create<TMetricOptions>( new() { ApiKey = apiKey } );
        using var handler = new DefaultAuthorizationHandler( options )
        {
            InnerHandler = new TestResponseHandler(),
        };

        using var invoker = new HttpMessageInvoker( handler );

        using var request = new HttpRequestMessage( HttpMethod.Get, new Uri( "https://localhost:8080" ) );
        using var response = await invoker.SendAsync( request, CancellationToken.None );

        Assert.Null( response.RequestMessage!.Headers.Authorization );
    }

    private sealed class TestResponseHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
            => new HttpResponseMessage
            {
                Content = new StringContent( "Hello, World!" ),
                ReasonPhrase = nameof( HttpStatusCode.OK ),
                RequestMessage = request,
                StatusCode = HttpStatusCode.OK,
            };
    }
}