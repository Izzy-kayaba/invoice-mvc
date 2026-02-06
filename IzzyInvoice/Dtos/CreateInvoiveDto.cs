using System.ComponentModel.DataAnnotations;

namespace IzzyInvoice.Dtos;

public record CreateInvoiceDto
{
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


    [Required]
    [MinLength(1)]
    public IReadOnlyCollection<CreateInvoiceLineDto> Lines { get; init; }
        = Array.Empty<CreateInvoiceLineDto>();


    [StringLength(500)]
    public string? Notes { get; init; }
}
