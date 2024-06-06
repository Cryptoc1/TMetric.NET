using Microsoft.Extensions.DependencyInjection;
using TMetric.Abstractions;

namespace TMetric.Tests;

public sealed class ServiceExtensionTests
{
    [Fact]
    public void Client_can_be_constructed( )
    {
        var services = new ServiceCollection()
            .AddTMetric()
            .BuildServiceProvider();

        var exception = Record.Exception( services.GetRequiredService<ITMetricApi> );

        Assert.Null( exception );
        Assert.IsNotType<InvalidOperationException>( exception );
    }
}