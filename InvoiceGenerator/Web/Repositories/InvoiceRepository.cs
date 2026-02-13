using InvoiceGenerator.Data;
using InvoiceGenerator.Models.Entities;
using InvoiceGenerator.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Repositories
{

    /// <summary>
    /// EF Core implementation of Invoice repository.
    /// </summary>
    public class InvoiceRepository: IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .ToListAsync();
        }

        Task<Invoice?> IInvoiceRepository.UpdateAsync(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        Task<bool> IInvoiceRepository.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
