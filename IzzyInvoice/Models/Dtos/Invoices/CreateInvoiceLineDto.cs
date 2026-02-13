using System.ComponentModel.DataAnnotations;

namespace IzzyInvoice.Dtos;

public record CreateInvoiceLineDto
{
    [Required]
    [StringLength(200)]
    public string Description { get; init; } = default!;

    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }

    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; init; }
}
