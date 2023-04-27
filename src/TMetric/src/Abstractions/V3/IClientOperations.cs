using System.ComponentModel.DataAnnotations;

namespace TMetric.Abstractions.V3;

public interface IClientOperations
{
    public Task<ClientBasic[]> Get( int accountId, CancellationToken cancellation = default );
}

public record class ClientBasic
{
    public Uri IconUrl { get; set; }

    public int Id { get; set; }

    [MaxLength( 255 )]
    public string Name { get; set; }
}