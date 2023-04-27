using System.ComponentModel.DataAnnotations;

namespace TMetric.Abstractions.V3;

public interface IClientOperations
{
    public Task<V3.Client[]> Get( int accountId, CancellationToken cancellation = default );
}

public record class Client
{
    public Uri IconUrl { get; set; }

    public int Id { get; set; }

    [MaxLength( 255 )]
    public string Name { get; set; }
}