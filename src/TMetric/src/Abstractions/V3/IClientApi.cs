using System.ComponentModel.DataAnnotations;

namespace TMetric.Abstractions.V3;

public interface IClientApi
{
    public Task<ClientBasic[]> Get( int accountId, CancellationToken cancellation = default );
}

public record class ClientBasic
{
    public Uri IconUrl { get; set; } = default!;

    public int Id { get; set; }

    [StringLength( 255 )]
    public string Name { get; set; } = default!;
}