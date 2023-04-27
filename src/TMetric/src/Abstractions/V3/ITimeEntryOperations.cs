﻿using System.ComponentModel.DataAnnotations;

namespace TMetric.Abstractions.V3;

public interface ITimeEntryOperations
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
    public DateTime? EndTime { get; set; }

    public int Id { get; set; }

    public bool IsBillable { get; set; }

    public bool IsInvoiced { get; set; }

    [MaxLength( 400 )]
    public string? Note { get; set; }

    public ProjectBasic? Project { get; set; }

    public DateTime? StartTime { get; set; }

    public TaskBasic? Task { get; set; }
}

public record class ProjectBasic
{
    public ClientBasic Client { get; set; }

    public Uri IconUrl { get; set; }

    public int Id { get; set; }

    [MaxLength( 255 )]
    public string Name { get; set; }

    public ProjectStatus Status { get; set; }
}

public record class TaskBasic
{
    public ExternalLink? ExternalLink { get; set; }

    public int Id { get; set; }

    public IntegrationBasic? Integration { get; set; }

    [MaxLength( 400 )]
    public string Name { get; set; }
}

public record class ExternalLink
{
    [Required]
    public string Caption { get; set; }

    [Required]
    public Uri IconUrl { get; set; }

    [MinLength( 1 )]
    [Required]
    public string IssueId { get; set; }

    [Required]
    public Uri Link { get; set; }
}

public record class IntegrationBasic
{
    public Uri Url { get; set; }

    public string Type { get; set; }
}