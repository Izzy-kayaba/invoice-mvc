using InvoiceGenerator.Data;
using InvoiceGenerator.Models.Entities;
using InvoiceGenerator.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Repositories
{

    /// <summary>
    /// EF Core implementation of Invoice repository.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice).ConfigureAwait(true);
            await _context.SaveChangesAsync().ConfigureAwait(true);
            return invoice;
        }

        public async Task<Invoice?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId).ConfigureAwait(true);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync(Guid userId)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .Where(i => i.UserId == userId)
                .ToListAsync()
                .ConfigureAwait(true);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (invoice == null)
                return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Invoice?> UpdateAsync(Invoice invoice)
        {
            var existingInvoice = await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == invoice.Id && i.UserId == invoice.UserId);

            if (existingInvoice == null)
                return null;

            _context.Entry(existingInvoice).CurrentValues.SetValues(invoice);

            await _context.SaveChangesAsync();
            return existingInvoice;
        }
    }
}
