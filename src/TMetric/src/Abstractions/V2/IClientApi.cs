using System.ComponentModel.DataAnnotations;

namespace TMetric.Abstractions.V2;

public interface IClientApi
{
    public Task<Client[]> Get( int accountId, CancellationToken cancellation = default );

    public Task<Client> Get( int accountId, int clientId, CancellationToken cancellation = default );
}

public record class Client
{
    public int AccountId { get; set; }

    public uint ActiveProjectsCount { get; set; }

    [StringLength( 100 )]
    public string? Avatar { get; set; }

    [StringLength( 400 )]
    public string? ClientAddress { get; set; }

    public int ClientId { get; set; }

    [StringLength( 255 )]
    public string? ClientName { get; set; }

    public int[]? ContactUsers { get; set; }

    public Rate DefaultBillableRate { get; set; } = default!;

    public uint TotalProjectsCount { get; set; }
}

public record class Rate
{
    public decimal Amount { get; set; }

    [StringLength( 3, MinimumLength = 3 )]
    public string Currency { get; set; } = default!;
}