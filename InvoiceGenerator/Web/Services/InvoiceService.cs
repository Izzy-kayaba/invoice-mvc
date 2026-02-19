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

        public async Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequestDto dto)
        {
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
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

            return new InvoiceResponseDto
            {
                Id = createdInvoice.Id,
                InvoiceNumber = createdInvoice.InvoiceNumber,
                IssueDate = createdInvoice.IssueDate,
                DueDate = createdInvoice.DueDate,
                CustomerName = createdInvoice.CustomerName,
                CustomerEmail = createdInvoice.CustomerEmail,
                TotalAmount = createdInvoice.TotalAmount,
                Currency = createdInvoice.Currency,
                Status = createdInvoice.Status,
                Notes = createdInvoice.Notes,
                Lines = createdInvoice.Items.Select(i => new InvoiceLineDto
                {
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }


        public async Task<InvoiceResponseDto?> GetByIdAsync(Guid id)
        {
            var invoice = await _repository.GetByIdAsync(id).ConfigureAwait(true);

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

    }
}
