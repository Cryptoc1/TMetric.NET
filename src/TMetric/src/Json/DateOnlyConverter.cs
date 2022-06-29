using System.Text.Json;
using System.Text.Json.Serialization;

namespace TMetric.Json;

/// <summary> Converter that supports conversion of <see cref="DateOnly"/>. </summary>
public sealed class DateOnlyConverter : JsonConverter<DateOnly>
{
    /// <inheritdoc/>
    public override DateOnly Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
        => reader.TryGetDateTime( out var date )
            ? DateOnly.FromDateTime( date )
            : DateOnly.Parse( reader.GetString()! );

    /// <inheritdoc/>
    public override void Write( Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options )
        => writer.WriteStringValue( value.ToDateTime( TimeOnly.MinValue ) );
}