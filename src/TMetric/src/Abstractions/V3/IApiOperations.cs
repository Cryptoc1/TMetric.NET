namespace TMetric.Abstractions.V3;

public interface IApiOperations
{
    IClientOperations Clients { get; }

    ITimeEntryOperations TimeEntries { get; }
}