namespace TMetric.Abstractions.V3;

public interface IVersion3Api
{
    IClientApi Clients { get; }

    ITimeEntryApi TimeEntries { get; }
}