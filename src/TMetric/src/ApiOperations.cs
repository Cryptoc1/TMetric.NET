using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric;

public abstract class ApiOperations
{
    protected readonly HttpClient Http;
    protected readonly IOptions<TMetricOptions> Options;

    protected ApiOperations( HttpClient http, IOptions<TMetricOptions> options )
    {
        Http = http;
        Options = options;
    }
}