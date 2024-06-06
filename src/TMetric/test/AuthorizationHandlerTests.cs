using System.Net;
using TMetric.Abstractions;

namespace TMetric.Tests;

public sealed class AuthorizationHandlerTests
{
    [Fact]
    public async Task Handler_adds_bearer_authorization_header( )
    {

        using var handler = new AuthorizationHandler( new TestApiCredential( "TEST" ) )
        {
            InnerHandler = new TestResponseHandler(),
        };

        using var invoker = new HttpMessageInvoker( handler );

        using var request = new HttpRequestMessage( HttpMethod.Get, new Uri( "https://localhost:8080" ) );
        using var response = await invoker.SendAsync( request, CancellationToken.None );

        var authorization = response.RequestMessage!.Headers.Authorization;

        Assert.NotNull( authorization );
        Assert.Equal( "Bearer", authorization!.Scheme );
        Assert.Equal( "TEST", authorization!.Parameter );
    }

    [Theory]
    [InlineData( "" )]
    [InlineData( null )]
    [InlineData( " " )]
    public async Task Handler_does_not_add_empty_bearer_authorization_header( string? apiKey )
    {
        using var handler = new AuthorizationHandler( new TestApiCredential( apiKey ) )
        {
            InnerHandler = new TestResponseHandler(),
        };

        using var invoker = new HttpMessageInvoker( handler );

        using var request = new HttpRequestMessage( HttpMethod.Get, new Uri( "https://localhost:8080" ) );
        using var response = await invoker.SendAsync( request, CancellationToken.None );

        Assert.Null( response.RequestMessage!.Headers.Authorization );
    }

    private sealed class TestApiCredential( string? apiKey ) : ApiCredential
    {
        public override ValueTask<string> Acquire( CancellationToken cancellation ) => new( apiKey! );
    }

    private sealed class TestResponseHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
            => Task.FromResult( new HttpResponseMessage
            {
                Content = new StringContent( "Hello, World!" ),
                ReasonPhrase = nameof( HttpStatusCode.OK ),
                RequestMessage = request,
                StatusCode = HttpStatusCode.OK,
            } );
    }
}