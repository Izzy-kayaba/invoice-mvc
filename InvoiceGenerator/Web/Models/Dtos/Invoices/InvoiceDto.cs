using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Web.Models.Dtos.Invoices;

public record InvoiceDto
{
    [Required]
    public Guid Id { get; init; }

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

    [Range(0.01, double.MaxValue)]
    public decimal TotalAmount { get; init; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; init; } = "ZAR";

    [MinLength(1)]
    public IReadOnlyCollection<InvoiceLineDto> Lines { get; init; } = Array.Empty<InvoiceLineDto>();

    [Required]
    public InvoiceStatus Status { get; init; }

    [StringLength(500)]
    public string? Notes { get; init; }
}
