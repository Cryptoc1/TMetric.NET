using Microsoft.Extensions.DependencyInjection;
using TMetric.Abstractions;
using V2 = TMetric.Abstractions.V2;
using V3 = TMetric.Abstractions.V3;

namespace TMetric;

/// <summary> Extensions to <see cref="IServiceCollection"/> for registering and configuring TMetric services. </summary>
public static class TMetricServiceExtensions
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

        _ = services.AddTransient<ITMetricClient, TMetricClient>()
            .AddTransient<AuthorizationHandler, DefaultAuthorizationHandler>()

            .AddHttpClient( Strings.ApiClientName, http => http.BaseAddress = new Uri( Strings.DefaultApiAddress ) )
            .AddHttpMessageHandler<AuthorizationHandler>()
            .AddTypedClient<IVersion2Api, Version2Api>()
            .AddTypedClient<V2.IClientOperations, ClientV2Operations>()
            .AddTypedClient<V2.IInvoiceOperations, InvoiceOperations>()
            .AddTypedClient<V2.IProjectOperations, ProjectOperations>()
            .AddTypedClient<IVersion3Api, Version3Api>()
            .AddTypedClient<V3.IClientOperations, ClientV3Operations>();

        return services;
    }
}