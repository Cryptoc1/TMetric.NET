using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric;

/// <summary> The default implementation of <see cref="AuthorizationHandler"/>, providing an API Key via <see cref="TMetricOptions.ApiKey"/>. </summary>
public sealed class DefaultAuthorizationHandler : AuthorizationHandler
{
    private readonly IOptions<TMetricOptions> options;

    public DefaultAuthorizationHandler( IOptions<TMetricOptions> options )
        => this.options = options;

    /// <inheritdoc />
    protected override ValueTask<string> GetApiKey( CancellationToken cancellation )
        => new( options.Value.ApiKey );
}