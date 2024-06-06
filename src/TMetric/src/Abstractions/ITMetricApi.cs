namespace TMetric.Abstractions;

/// <summary> Describes a TMetric Api Client. </summary>
public interface ITMetricApi
{
    /// <summary> Version 2 Apis. </summary>
    V2.IVersion2Api V2 { get; }

    /// <summary> Version 3 Apis. </summary>
    V3.IVersion3Api V3 { get; }
}