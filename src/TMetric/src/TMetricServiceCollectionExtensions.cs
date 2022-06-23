using Microsoft.Extensions.DependencyInjection;
using TMetric.Abstractions;
using TMetric.Json;

namespace TMetric;

public static class TMetricServiceCollectionExtensions
{
    public static IServiceCollection AddTMetric( this IServiceCollection services, Action<TMetricOptions>? configure = null )
    {
        ArgumentNullException.ThrowIfNull( services );

        var optionsBuilder = services.AddOptions<TMetricOptions>()
            .Configure( ConfigureDefaultOptions )
            .ValidateDataAnnotations();

        if( configure is not null )
        {
            _ = optionsBuilder.Configure( configure );
        }

        _ = services.AddTransient<AuthorizationHandler, DefaultAuthorizationHandler>()
            .AddHttpClient<ITMetricClient, TMetricClient>( Strings.ApiClientName, http => http.BaseAddress = new Uri( Strings.DefaultApiAddress ) )
            .AddHttpMessageHandler<AuthorizationHandler>()
            .AddTypedClient<IClientOperations, ClientOperations>()
            .AddTypedClient<IInvoiceOperations, InvoiceOperations>();

        return services;
    }

    private static void ConfigureDefaultOptions( TMetricOptions options )
    {
        ArgumentNullException.ThrowIfNull( options );

        options.SerializerOptions.Converters.Add( new DateOnlyConverter() );
    }
}