using System.Text;

namespace TMetric;

public sealed class QueryStringBuilder
{
    private readonly StringBuilder builder;

    public QueryStringBuilder( )
        => builder = new( "?" );

    public QueryStringBuilder Add( string name, string value )
    {
        _ = builder.Append( $"{name}={Uri.EscapeDataString( value )}&" );
        return this;
    }

    public QueryStringBuilder Add( string name, IEnumerable<string> value )
    {
        foreach( string? item in value )
        {
            _ = builder.Append( $"{name}={Uri.EscapeDataString( item )}&" );
        }

        return this;
    }

    public override string ToString( )
        => builder.ToString().TrimEnd( '&' );

    public static implicit operator string( QueryStringBuilder builder ) => builder.ToString();
}