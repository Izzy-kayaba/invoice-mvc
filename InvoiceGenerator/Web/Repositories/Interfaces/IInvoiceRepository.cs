using InvoiceGenerator.Models.Entities;

namespace InvoiceGenerator.Repositories.Interfaces;

/// Defines persistence operations for invoices.
public interface IInvoiceRepository
{
    Task<Invoice> CreateAsync(Invoice invoice);

    Task<Invoice?> GetByIdAsync(Guid id);

    Task<List<Invoice>> GetAllAsync();

    Task<Invoice?> UpdateAsync(Invoice invoice);

    Task<bool> DeleteAsync(Guid id);

}