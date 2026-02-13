using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceGenerator.Models.Entities;


/// <summary>
/// Represents each line item belonging to an invoice.
/// </summary>
public class InvoiceItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal { get; set; }

    // Foreign Key
    public Guid InvoiceId { get; set; }

    [ForeignKey("InvoiceId")]
    public Invoice? Invoice { get; set; }
}
