using Microsoft.Extensions.DependencyInjection;
using TMetric.Abstractions;

namespace TMetric;

/// <summary> Extensions to <see cref="IServiceCollection"/> for registering and configuring TMetric services. </summary>
public static class TMetricServiceCollectionExtensions
{
    /// <summary> Add TMetric services to the given <paramref name="services"/>. </summary>
    /// <param name="services"> The service collection TMetric services are to be added. </param>
    /// <param name="configure"> A delegate that may configure TMetric service options. </param>
    public static IServiceCollection AddTMetric( this IServiceCollection services, Action<TMetricOptions>? configure = null )
    {
        ArgumentNullException.ThrowIfNull( services );

        var optionsBuilder = services.AddOptions<TMetricOptions>()
            .ValidateDataAnnotations();

        if( configure is not null )
        {
            _ = optionsBuilder.Configure( configure );
        }

        _ = services.AddTransient<AuthorizationHandler, DefaultAuthorizationHandler>()
            .AddHttpClient<ITMetricClient, TMetricClient>( Strings.ApiClientName, http => http.BaseAddress = new Uri( Strings.DefaultApiAddress ) )
            .AddHttpMessageHandler<AuthorizationHandler>()
            .AddTypedClient<IClientOperations, ClientOperations>()
            .AddTypedClient<IInvoiceOperations, InvoiceOperations>()
            .AddTypedClient<IProjectOperations, ProjectOperations>();

        return services;
    }
}