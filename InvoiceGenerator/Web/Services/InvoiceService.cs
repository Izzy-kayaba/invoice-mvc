using InvoiceGenerator.Models.Dtos.Invoices;
using InvoiceGenerator.Models.Entities;
using InvoiceGenerator.Models.Enums;
using InvoiceGenerator.Repositories.Interfaces;
using InvoiceGenerator.Services.Interfaces;
using InvoiceGenerator.Web.Models.Dtos.Invoices;

namespace InvoiceGenerator.Services
{
    /// <summary>
    /// Concrete implementation of invoice business logic.
    /// This class knows NOTHING about HTTP.
    /// </summary>

    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;

        public InvoiceService(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequestDto dto, Guid userId)
        {
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                InvoiceNumber = dto.InvoiceNumber,
                IssueDate = dto.IssueDate,
                DueDate = dto.DueDate,
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                Currency = dto.Currency,
                Status = InvoiceStatus.Draft,
                Notes = dto.Notes,
            };

            invoice.Items = dto.Lines?.Select(i => new InvoiceItem
            {
                Id = Guid.NewGuid(),
                Description = i.Description,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Invoice = invoice   // Required property satisfied
            }).ToList() ?? new List<InvoiceItem>();

            invoice.TotalAmount = invoice.Items.Sum(i => i.Quantity * i.UnitPrice);

            var createdInvoice = await _repository.CreateAsync(invoice).ConfigureAwait(true);

            return MapToResponse(createdInvoice);
        }

        public async Task<IEnumerable<InvoiceResponseDto>> GetAllAsync(Guid userId)
        {
            var invoices = await _repository.GetAllAsync(userId).ConfigureAwait(true);

            return invoices.Select(invoice => new InvoiceResponseDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                CustomerName = invoice.CustomerName,
                CustomerEmail = invoice.CustomerEmail,
                TotalAmount = invoice.TotalAmount,
                Currency = invoice.Currency,
                Status = invoice.Status,
                Notes = invoice.Notes,
                Lines = invoice.Items.Select(i => new InvoiceLineDto
                {
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            });
        }

        public async Task<InvoiceResponseDto?> GetByIdAsync(Guid id, Guid userId)
        {
            var invoice = await _repository.GetByIdAsync(id, userId).ConfigureAwait(true);

            if (invoice == null)
                return null;

            return new InvoiceResponseDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                CustomerName = invoice.CustomerName,
                CustomerEmail = invoice.CustomerEmail,
                TotalAmount = invoice.TotalAmount,
                Currency = invoice.Currency,
                Status = invoice.Status,
                Notes = invoice.Notes,
                Lines = invoice.Items.Select(i => new InvoiceLineDto
                {
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public Task<InvoiceResponseDto?> GetByIdAsync(Guid id, Guid userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInvoiceAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        private static InvoiceResponseDto MapToResponse(Invoice invoice)
        {
            return new InvoiceResponseDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                CustomerName = invoice.CustomerName,
                CustomerEmail = invoice.CustomerEmail,
                TotalAmount = invoice.TotalAmount,
                Currency = invoice.Currency,
                Status = invoice.Status,
                Notes = invoice.Notes,
                Lines = invoice.Items.Select(i => new InvoiceLineDto
                {
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }
    }
}
