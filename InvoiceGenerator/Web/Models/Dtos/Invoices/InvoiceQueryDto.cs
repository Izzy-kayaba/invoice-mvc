using System.ComponentModel.DataAnnotations;

using InvoiceGenerator.Models.Enums;

public class InvoiceQueryDto
{
    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, 100)]
    public int PageSize { get; init; } = 10;

    public string? SortBy { get; init; } = "IssueDate";
    public bool Desc { get; init; } = true;
    
    [StringLength(50)]
    public string? InvoiceNumber { get; init; }

    [EmailAddress]
    public string? CustomerEmail { get; init; }

    public InvoiceStatus? Status { get; init; }

    public DateTime? IssueDateFrom { get; init; }
    
    public DateTime? IssueDateTo { get; init; }

    [Range(0.01, double.MaxValue)]
    public decimal? MinTotal { get; init; }
    public decimal? MaxTotal { get; init; }
}
