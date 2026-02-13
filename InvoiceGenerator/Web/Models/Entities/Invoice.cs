using System.ComponentModel.DataAnnotations;
using InvoiceGenerator.Models.Enums;

namespace InvoiceGenerator.Models.Entities;

/// <summary>
/// Represents the core business entity for an invoice.
/// This is NOT a DTO.
/// This is domain truth.
/// </summary>
/// 
/// <summary>
/// Represents the Invoice table in the database.
/// </summary>
public class Invoice
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    public DateTime IssueDate { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "ZAR";

    public decimal TotalAmount { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

    [MaxLength(500)]
    public string? Notes { get; set; }

    public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
}
