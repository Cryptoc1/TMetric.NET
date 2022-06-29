using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using TMetric.Json;

namespace TMetric.Abstractions;

public class TMetricOptions
{
    [Required]
    public string ApiKey { get; set; }

    public JsonSerializerOptions SerializerOptions { get; set; } = new( JsonSerializerDefaults.Web )
    {
        Converters = { new DateOnlyConverter() },
    };
}