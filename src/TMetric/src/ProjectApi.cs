using ESCd.Extensions.Http;
using TMetric.Abstractions.V2;
using TMetric.Json;

namespace TMetric;

internal sealed class ProjectApi( HttpClient http ) : IProjectApi
{
    public Task<ProjectLite[]> Get( int accountId, GetProjectsParameters parameters, CancellationToken cancellation = default )
    {
        ArgumentNullException.ThrowIfNull( parameters );

        var query = new QueryStringBuilder()
            .Append( "onlyTracked", parameters.OnlyTracked );

        if( parameters.Clients?.Count is not (null or 0) )
        {
            foreach( var client in parameters.Clients ) query.Append( "ClientList", client );
        }

        return http.GetFromJsonAsync(
            $"accounts/{accountId}/projects{query}",
            TMetricJsonContext.Default.ProjectLiteArray,
            cancellation )!;
    }

    public Task<Project> Get( int accountId, int projectId, CancellationToken cancellation = default ) => http.GetFromJsonAsync(
            $"accounts/{accountId}/projects/{projectId}",
            TMetricJsonContext.Default.Project,
            cancellation )!;
}