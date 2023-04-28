using Microsoft.Extensions.DependencyInjection;
using TMetric.Abstractions;

namespace TMetric.Tests;

public sealed class ServiceExtensionTests
{
    [Fact]
    public void Client_can_be_constructed( )
    {
        var services = new ServiceCollection()
            .AddTMetric( options => options.ApiKey = "TEST" )
            .BuildServiceProvider();

        var exception = Record.Exception( services.GetRequiredService<ITMetricClient> );

        Assert.Null( exception );
        Assert.IsNotType<InvalidOperationException>( exception );
    }
}