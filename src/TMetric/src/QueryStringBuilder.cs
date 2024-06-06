using System.Globalization;
using System.Text;

namespace TMetric;

/// <summary> Provides a fluent builder syntax for creating URL Query Strings. </summary>
public sealed class QueryStringBuilder
{
    private readonly StringBuilder builder = new( "?" );

    /// <summary> Add the given <paramref name="value"/> to the current query string. </summary>
    /// <param name="name"> The name of the query parameter to add the given <paramref name="value"/> for. </param>
    /// <param name="value"> The query parameter value to add. </param>
    public QueryStringBuilder Add( string name, string value )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace( name );

        builder.Append( CultureInfo.InvariantCulture, $"{name}={Uri.EscapeDataString( value )}&" );
        return this;
    }

    /// <summary> Add the given <paramref name="values"/> to the current query string. </summary>
    /// <param name="name"> The name of the query parameter to add the given <paramref name="values"/> for. </param>
    /// <param name="values"> The query parameter values to add. </param>
    public QueryStringBuilder Add( string name, IEnumerable<string> values )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace( name );
        foreach( var item in values )
        {
            builder.Append( CultureInfo.InvariantCulture, $"{name}={Uri.EscapeDataString( item )}&" );
        }

        return this;
    }

    /// <summary> Build the current query string value. </summary>
    public override string ToString( )
        => builder.ToString().TrimEnd( '&' );

    /// <summary> Build the query string represented by the given <paramref name="builder"/>. </summary>
    /// <param name="builder"> The <see cref="QueryStringBuilder"/> representing the query string to build. </param>
    public static implicit operator string( QueryStringBuilder builder ) => builder.ToString();
}

public static class QueryStringBuilderExtensions
{
    public static QueryStringBuilder Add( this QueryStringBuilder builder, string name, bool? value )
    {
        ArgumentNullException.ThrowIfNull( builder );
        ArgumentException.ThrowIfNullOrWhiteSpace( name );

        if( value.HasValue )
        {
            return builder.Add( name, value.Value.ToString( CultureInfo.InvariantCulture ) );
        }

        return builder;
    }

    public static QueryStringBuilder Add( this QueryStringBuilder builder, string name, int? value )
    {
        ArgumentNullException.ThrowIfNull( builder );
        ArgumentException.ThrowIfNullOrWhiteSpace( name );

        if( value.HasValue )
        {
            return builder.Add( name, value.Value.ToString( CultureInfo.InvariantCulture ) );
        }

        return builder;
    }
}