
using InvoiceGenerator.Models.Dtos.Invoices;
using InvoiceGenerator.Web.Models.Dtos.Invoices;

namespace InvoiceGenerator.Services.Interfaces
{
    /// Defines invoice-related business operations.
    /// Controllers depend on this interface, not implementation.
    public interface IInvoiceService
    {
        Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequestDto dto);
        Task<InvoiceResponseDto?> GetByIdAsync(Guid id);
    }
}
