using System.ComponentModel.DataAnnotations;
using InvoiceGenerator.Models.Dtos.Invoices;


namespace InvoiceGenerator.Web.Models.Dtos.Invoices;

public record CreateInvoiceRequestDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string InvoiceNumber { get; init; } = default!;

    [Required]
    public DateTime IssueDate { get; init; }

    [Required]
    public DateTime DueDate { get; init; }

    [Required]
    [StringLength(150)]
    public string CustomerName { get; init; } = default!;

    [Required]
    [EmailAddress]
    public string CustomerEmail { get; init; } = default!;

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; init; } = "ZAR";

    [MinLength(1)]
    public IReadOnlyCollection<InvoiceLineDto> Lines { get; init; } = Array.Empty<InvoiceLineDto>();

    [StringLength(500)]
    public string? Notes { get; init; }
    public DateTime InvoiceDate { get; internal set; }
    public IEnumerable<object>? Items { get; internal set; }
}

