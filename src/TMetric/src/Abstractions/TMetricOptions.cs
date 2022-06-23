using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace TMetric.Abstractions;

public class TMetricOptions
{
    [Required]
    public string ApiKey { get; set; }

    public JsonSerializerOptions SerializerOptions { get; set; } = new( JsonSerializerDefaults.Web );
}