namespace TMetric.Abstractions;

/// <summary> Describes a type that can provide a TMetric API Key to be used for the authorization of requests. </summary>
public abstract class ApiCredential
{
    /// <summary> Retrieve the TMetric API Key to be used for authorization. </summary>
    public abstract ValueTask<string> Acquire( CancellationToken cancellation );
}