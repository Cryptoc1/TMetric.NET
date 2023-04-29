using System.ComponentModel.DataAnnotations;

namespace TMetric.Abstractions.V2;

public interface IProjectOperations
{
    Task<ProjectLite[]> Get( int accountId, GetProjectsParameters parameters, CancellationToken cancellation = default );

    Task<Project> Get( int accountId, int projectId, CancellationToken cancellation = default );
}

public record class GetProjectsParameters
{
    public ICollection<int> Clients { get; set; } = new List<int>();

    public bool? OnlyTracked { get; set; }
}

public record class Project
{
    public int AccountId { get; set; }

    public Uri Avatar { get; set; }

    public int BudgetAlertPercents { get; set; }

    public DateTimeOffset BudgetAppliedDate { get; set; }

    public double BudgetSize { get; set; }

    public int ClientId { get; set; }

    public Rate CommonBillableRate { get; set; }

    public bool IsBillable { get; set; }

    public string? Notes { get; set; }

    public string ProjectCode { get; set; }

    public double ProjectFee { get; set; }

    public int ProjectId { get; set; }

    public string ProjectName { get; set; }

    public ProjectStatus ProjectStatus { get; set; }
}

public record class ProjectLite
{
    [MaxLength( 100 )]
    public string? Avatar { get; set; }

    public int AccountId { get; set; }

    public decimal BillableAmount { get; set; }

    public string? BillableCurrency { get; set; }

    public string? BudgetCurrency { get; set; }

    public decimal BudgetSize { get; set; }

    public int? ClientId { get; set; }

    public int GroupCount { get; set; }

    public bool IsBillable { get; set; }

    public int MemberCount { get; set; }

    public string? Notes { get; set; }

    [MaxLength( 16 )]
    public string? ProjectCode { get; set; }

    public decimal ProjectFee { get; set; }

    public int ProjectId { get; set; }

    [MaxLength( 255 )]
    public string? ProjectName { get; set; }

    public ProjectStatus ProjectStatus { get; set; }

    public decimal SpentBudget { get; set; }

    public decimal TotalBudget { get; set; }
}