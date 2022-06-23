using System.Text.Json;
using System.Text.Json.Serialization;

namespace TMetric.Json;

public sealed class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        var date = reader.GetDateTime();
        return DateOnly.FromDateTime( date );
    }

    public override void Write( Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options ) => throw new NotImplementedException();
}