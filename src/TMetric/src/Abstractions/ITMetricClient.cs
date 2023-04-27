namespace TMetric.Abstractions;

public interface ITMetricClient
{
    V2.IApiOperations V2 { get; }

    V3.IApiOperations V3 { get; }
}