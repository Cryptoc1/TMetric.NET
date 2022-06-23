namespace TMetric.Abstractions;

public interface IClientOperations
{
    Task<Client[]> Get( int accountId, CancellationToken cancellation = default );

    Task<Client> Get( int accountId, int clientId, CancellationToken cancellation = default );
}

public record class Client
{
    public int AccountId { get; set; }

    public uint ActiveProjectsCount { get; set; }

    public string Avatar { get; set; }

    public string ClientAddress { get; set; }

    public int ClientId { get; set; }

    public string ClientName { get; set; }

    public int[] ContactUsers { get; set; }

    public Rate DefaultBillableRate { get; set; }

    public uint TotalProjectsCount { get; set; }
}

public record class Rate
{
    public decimal Amount { get; set; }

    public string Currency { get; set; }
}