using InvoiceGenerator.Models.Entities;

namespace InvoiceGenerator.Repositories.Interfaces
{
    /// Defines persistence operations for invoices.
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateAsync(Invoice invoice);
        Task<IEnumerable<Invoice>> GetAllAsync(Guid userId);
        Task<Invoice?> GetByIdAsync(Guid id, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<Invoice?> UpdateAsync(Invoice invoice);
    }
}