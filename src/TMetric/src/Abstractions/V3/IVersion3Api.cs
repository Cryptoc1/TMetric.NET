namespace TMetric.Abstractions.V3;

public interface IVersion3Api
{
    public IClientApi Clients { get; }
    public ITimeEntryApi TimeEntries { get; }
}