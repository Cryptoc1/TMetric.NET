using System.Text.Json;
using TMetric.Json;

namespace TMetric.Tests.Json;

public sealed class DateOnlyConverterTests
{
    [Theory]
    [ClassData( typeof( DateConverterData ) )]
    public void Converter_reads_date( DateOnly expected, string value, string dateTimeValue )
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new DateOnlyConverter() },
        };

        var date = JsonSerializer.Deserialize<DateOnly>( value, options );
        Assert.Equal( expected, date );

        date = JsonSerializer.Deserialize<DateOnly>( dateTimeValue, options );
        Assert.Equal( expected, date );
    }

    [Theory]
    [ClassData( typeof( DateConverterData ) )]
    public void Converter_writes_date_as_datetime( DateOnly date, string value, string dateTimeValue )
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new DateOnlyConverter() },
        };

        string json = JsonSerializer.Serialize( date, options );

        Assert.NotEqual( value, json );
        Assert.Equal( dateTimeValue, json );
    }

    private sealed class DateConverterData : IEnumerable<object[]>
    {
        private static readonly DateOnly[] Dates = new[]
        {
            new DateOnly( 2022, 1, 2 ),
            new DateOnly( 2022, 2, 1 ),
            new DateOnly( 2022, 6, 6 ),
            new DateOnly( 2022, 6, 29 ),
        };

        public IEnumerator<object[]> GetEnumerator( )
        {
            foreach( var date in Dates )
            {
                yield return new object[] { date, $@"""{date}""", $@"""{date.ToDateTime( TimeOnly.MinValue ):yyyy-MM-ddTHH:mm:ss}""" };
            }
        }

        IEnumerator IEnumerable.GetEnumerator( ) => GetEnumerator();
    }
}