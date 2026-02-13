using InvoiceGenerator.Web.Models.Dtos.Invoices;

namespace InvoiceGenerator.Models.Dtos.Invoices
{
    public record InvoiceResponseDto
    {
        public Guid Id { get; init; }
        public string InvoiceNumber { get; init; } = default!;
        public DateTime IssueDate { get; init; }
        public DateTime DueDate { get; init; }
        public string CustomerName { get; init; } = default!;
        public string CustomerEmail { get; init; } = default!;
        public decimal TotalAmount { get; init; }
        public string Currency { get; init; } = default!;
        public IReadOnlyCollection<InvoiceLineDto> Lines { get; init; } = Array.Empty<InvoiceLineDto>();
        public InvoiceStatus Status { get; init; }
        public string? Notes { get; init; }
    }

}
