using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using TMetric.Abstractions;

namespace TMetric;

public sealed class ProjectOperations : IProjectOperations
{
    private readonly HttpClient http;
    private readonly IOptions<TMetricOptions> optionsAccessor;

    public ProjectOperations( HttpClient http, IOptions<TMetricOptions> optionsAccessor )
    {
        this.http = http;
        this.optionsAccessor = optionsAccessor;
    }

    public Task<ProjectLite[]> Get( int accountId, GetProjectsParameters parameters, CancellationToken cancellation = default )
    {
        ArgumentNullException.ThrowIfNull( parameters );
        Validator.ValidateObject( parameters, new( parameters ) );

        var query = new QueryStringBuilder();
        if( parameters.Clients?.Any() is true )
        {
            foreach( int client in parameters.Clients )
            {
                _ = query.Add( "ClientList", client.ToString() );
            }
        }

        if( parameters.OnlyTracked.HasValue )
        {
            _ = query.Add( "onlyTracked", parameters.OnlyTracked.Value.ToString() );
        }

        return http.GetFromJsonAsync<ProjectLite[]>( $"accounts/{accountId}/projects{query}", optionsAccessor.Value.SerializerOptions, cancellation )!;
    }
}