namespace TMetric.Tests;

public sealed class QueryStringBuilderTests
{
    [Theory]
    [InlineData( "key", "value", "?key=value" )]
    [InlineData( "key", "Hello World", "?key=Hello%20World" )]
    public void Builder_creates_query_string( string key, string value, string queryString )
    {
        var builder = new QueryStringBuilder()
            .Add( key, value );

        Assert.Equal( queryString, builder.ToString() );
    }

    [Fact]
    public void Builder_adds_multiple_values( )
    {
        var builder = new QueryStringBuilder()
            .Add( "key", new[] { "value", "value1" } );

        Assert.Equal( "?key=value&key=value1", builder.ToString() );
    }
}