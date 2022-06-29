using System.ComponentModel.DataAnnotations;

namespace TMetric.Abstractions;

public interface IInvoiceOperations
{
    Task<Invoice> Create( int accountId, CreateInvoiceParameters parameters, CancellationToken cancellation = default );

    Task<InvoiceExcel> Excel( int accountId, int invoiceId, CancellationToken cancellation = default );

    Task<Invoice[]> Get( int accountId, GetInvoicesParameters parameters, CancellationToken cancellation = default );

    Task<Invoice> Get( int accountId, int invoiceId, CancellationToken cancellation = default );

    Task Put( int accountId, Invoice invoice, CancellationToken cancellation = default );
}

public record class CreateInvoiceParameters
{
    [Required]
    public int ClientId { get; set; }

    [Required]
    public InvoiceType InvoiceType { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
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

    [StringLength( 3, MinimumLength = 3 )]
    public string Currency { get; set; }

    public decimal DiscountAmount { get; set; }

    public double DiscountPercents { get; set; }

    public int DueDays { get; set; }

    public int InvoiceId { get; set; }

    public InvoiceType InvoiceType { get; set; }

    public DateOnly IssueDate { get; set; }

    public InvoiceItem[]? Items { get; set; }

    [MaxLength( 30 )]
    public string? PurchaseOrderNumber { get; set; }

    public InvoiceStatus Status { get; set; }

    [MaxLength( 200 )]
    public string? Subject { get; set; }

    public decimal SubtotalAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public double TaxPercents { get; set; }

    public decimal TotalAmount { get; set; }

    [MaxLength( 30 )]
    public string TextId { get; set; }
}

public sealed class InvoiceExcel : Stream
{
    public override bool CanRead => source.CanRead;

    public override bool CanSeek => source.CanSeek;

    public override bool CanWrite => source.CanWrite;

    public override long Length => source.Length;

    public string Name { get; private set; }

    public override long Position { get => source.Position; set => source.Position = value; }

    private readonly Stream source;

    public InvoiceExcel( string name, Stream source )
    {
        Name = name;
        this.source = source;
    }

    public override void Flush( ) => source.Flush();

    public override int Read( byte[] buffer, int offset, int count ) => source.Read( buffer, offset, count );

    public override long Seek( long offset, SeekOrigin origin ) => source.Seek( offset, origin );

    public override void SetLength( long value ) => source.SetLength( value );

    public override void Write( byte[] buffer, int offset, int count ) => source.Write( buffer, offset, count );
}

public record class InvoiceItem
{
    public string? Description { get; set; }

    public string? ItemType { get; set; }

    public decimal UnitAmount { get; set; }

    public decimal UnitCount { get; set; }

    public decimal UnitPrice { get; set; }
}

public enum InvoiceStatus : int
{
    Draft,
    Sent,
    Paid,
}

public enum InvoiceType : int
{
    ByPersonHours,
    ByProjectHours,
    ByTaskHours,
    DetailedLineItems,
}