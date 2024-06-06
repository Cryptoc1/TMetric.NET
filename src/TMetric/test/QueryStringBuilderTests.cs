namespace TMetric.Tests;

public sealed class QueryStringBuilderTests
{
    [Theory]
    [InlineData( "key", "value", "?key=value" )]
    [InlineData( "key", "Hello World", "?key=Hello%20World" )]
    public void Builder_creates_query_string( string key, string value, string expectedQueryString )
    {
        var builder = new QueryStringBuilder()
            .Add( key, value );

        Assert.Equal( expectedQueryString, builder.ToString() );
    }

    [Fact]
    public void Builder_adds_multiple_values( )
    {
        var builder = new QueryStringBuilder()
            .Add( "key", [ "value", "value1" ] );

        Assert.Equal( "?key=value&key=value1", builder.ToString() );
    }

    [Theory]
    [InlineData( "key", "value", "?key=value" )]
    [InlineData( "key", "Hello World", "?key=Hello%20World" )]
    public void Builder_supports_cast_to_string( string key, string value, string expectedQueryString )
    {
        var builder = new QueryStringBuilder()
            .Add( key, value );

        var cast = ( string )builder;
        var queryString = builder.ToString();

        Assert.Equal( queryString, cast );
        Assert.Equal( expectedQueryString, cast );
    }
}