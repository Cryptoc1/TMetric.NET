namespace TMetric;

public static class DateTimeExtensions
{
    public static string ToQueryString( this DateOnly date ) => date.ToString( "yyyyMMdd" );
}