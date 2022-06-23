using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TMetric.Abstractions;

public interface IInvoiceOperations
{
    Task<Invoice> Create( int accountId, CreateInvoiceParameters parameters, CancellationToken cancellation = default );

    Task<Invoice[]> Get( int accountId, GetInvoicesParameters parameters, CancellationToken cancellation = default );

    Task<Invoice> Get( int accountId, int invoiceId, CancellationToken cancellation = default );
}

public record class CreateInvoiceParameters
{
    [Required]
    public int ClientId { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    public ICollection<int> Projects { get; set; } = new List<int>();

    [Required]
    public DateTime StartTime { get; set; }
}

public record class GetInvoicesParameters
{
    public ICollection<int> Clients { get; set; } = new List<int>();

    public DateOnly? EndDate { get; set; }

    public DateOnly? StartDate { get; set; }

    public InvoiceStatus? Status { get; set; }
}

public record class Invoice
{
    public int AccountId { get; set; }

    public int ClientId { get; set; }

    public string Currency { get; set; }

    public decimal DiscountAmount { get; set; }

    public double DiscountPercents { get; set; }

    public uint DueDays { get; set; }

    public int InvoiceId { get; set; }

    public DateOnly IssueDate { get; set; }

    public InvoiceItem[] Items { get; set; }

    public string PurchaseOrderNumber { get; set; }

    public InvoiceStatus Status { get; set; }

    public string Subject { get; set; }

    public decimal SubtotalAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public double TaxPercents { get; set; }

    public decimal TotalAmount { get; set; }

    public string TextId { get; set; }
}

public record class InvoiceItem
{
    public string Description { get; set; }

    public string ItemType { get; set; }

    public decimal UnitAmount { get; set; }

    public decimal UnitCount { get; set; }

    public decimal UnitPrice { get; set; }
}

public enum InvoiceStatus
{
    Draft,
    Sent,
    Paid,
}