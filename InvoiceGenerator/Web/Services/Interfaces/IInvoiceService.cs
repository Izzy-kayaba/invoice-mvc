
using InvoiceGenerator.Models.Dtos.Invoices;
using InvoiceGenerator.Web.Models.Dtos.Invoices;

namespace InvoiceGenerator.Services.Interfaces
{
    /// Defines invoice-related business operations.
    /// Controllers depend on this interface, not implementation.
    public interface IInvoiceService
    {
        Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequestDto dto, Guid userId);

        Task<IEnumerable<InvoiceResponseDto>> GetAllAsync(Guid userId);

        Task<InvoiceResponseDto?> GetByIdAsync(Guid id, Guid userId);

        Task<bool> DeleteInvoiceAsync(Guid id, Guid userId);
    }
}
