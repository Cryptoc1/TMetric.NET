using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMetric.Abstractions.V3;

public interface ITimeEntryApi
{
    Task<TimeEntry[]> Get( int accountId, GetTimeEntriesParameters parameters, CancellationToken cancellation = default );
}

public record class GetTimeEntriesParameters
{
    public int? UserId { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? StartDate { get; set; }
}

public record class TimeEntry
{
    public DateTimeOffset? EndTime { get; set; }

    public int Id { get; set; }

    public bool IsBillable { get; set; }

    public bool IsInvoiced { get; set; }

    [MaxLength( 400 )]
    public string? Note { get; set; }

    public ProjectBasic? Project { get; set; }

    public DateTimeOffset? StartTime { get; set; }

    public TagBasic[] Tags { get; set; } = Array.Empty<TagBasic>();

    public TaskBasic? Task { get; set; }
}

public record class ProjectBasic
{
    public ClientBasic Client { get; set; } = default!;

    public Uri IconUrl { get; set; } = default!;

    public int Id { get; set; }

    [StringLength( 255 )]
    public string Name { get; set; } = default!;

    [JsonConverter( typeof( JsonStringEnumConverter<ProjectStatus> ) )]
    public ProjectStatus Status { get; set; }
}

public record class TagBasic
{
    public int Id { get; set; }

    public bool IsWorkType { get; set; }

    [StringLength( 50 )]
    public string Name { get; set; } = default!;
}

public record class TaskBasic
{
    public ExternalLink? ExternalLink { get; set; }

    public int Id { get; set; }

    public IntegrationBasic? Integration { get; set; }

    [StringLength( 400 )]
    public string Name { get; set; } = default!;
}

public record class ExternalLink
{
    [Required]
    public string Caption { get; set; } = default!;

    [Required]
    public Uri IconUrl { get; set; } = default!;

    [StringLength( 1 )]
    [Required]
    public string IssueId { get; set; } = default!;

    [Required]
    public Uri Link { get; set; } = default!;
}

public record class IntegrationBasic
{
    public Uri Url { get; set; } = default!;

    public string Type { get; set; } = default!;
}