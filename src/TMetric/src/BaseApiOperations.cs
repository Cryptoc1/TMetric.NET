using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric;

public abstract class BaseApiOperations
{
    protected readonly HttpClient Http;
    protected readonly IOptions<TMetricOptions> Options;

    protected BaseApiOperations( HttpClient http, IOptions<TMetricOptions> options )
    {
        Http = http;
        Options = options;
    }
}