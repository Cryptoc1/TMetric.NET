using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMetric.Abstractions;

namespace TMetric;

/// <summary> Extensions to <see cref="IServiceCollection"/> for registering and configuring TMetric services. </summary>
public static class TMetricServiceExtensions
{
    public static IServiceCollection AddTMetric( this IServiceCollection services, Action<IHttpClientBuilder>? configure = default )
    {
        ArgumentNullException.ThrowIfNull( services );

        var builder = services.AddHttpClient<ITMetricApi, TMetricApi>( http => http.BaseAddress = new Uri( TMetricDefaults.ApiAddress ) );
        configure?.Invoke( builder );

        return services;
    }

    public static IHttpClientBuilder BindApiCredential( this IHttpClientBuilder builder, string key = "TMetric:ApiKey" )
    {
        ArgumentNullException.ThrowIfNull( builder );
        ArgumentException.ThrowIfNullOrWhiteSpace( key );

        return builder.AddHttpMessageHandler(
            serviceProvider => new AuthorizationHandler(
                new BoundApiCredential( serviceProvider.GetRequiredService<IConfiguration>(), key ) ) );
    }

    public static IHttpClientBuilder UseApiCredential<[DynamicallyAccessedMembers( DynamicallyAccessedMemberTypes.PublicConstructors )] TCredential>( this IHttpClientBuilder builder )
        where TCredential : ApiCredential
    {
        ArgumentNullException.ThrowIfNull( builder );

        return builder.AddHttpMessageHandler(
            serviceProvider => new AuthorizationHandler(
                ActivatorUtilities.GetServiceOrCreateInstance<TCredential>( serviceProvider ) ) );
    }
}

internal sealed class BoundApiCredential( IConfiguration configuration, string key ) : ApiCredential
{
    private readonly string value = configuration[ key ] ?? throw new ArgumentException( $"Configuration does not contain the key '{key}'.", nameof( key ) );

    public override ValueTask<string> Acquire( CancellationToken cancellation ) => new( value );
}