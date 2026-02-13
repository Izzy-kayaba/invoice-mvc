using InvoiceGenerator.Web.Models.Dtos.Invoices;
using InvoiceGenerator.Web.Services.Interfaces;

namespace InvoiceGenerator.Services;

/// Concrete implementation of invoice business logic.
/// This class knows NOTHING about HTTP.

public class InvoiceService : IInvoiceService
{
    public InvoiceDto CreateInvoice(CreateInvoiceRequestDto dto)
    {
        return new InvoiceDto
        {
            Id = Guid.NewGuid(),
            InvoiceNumber = dto.InvoiceNumber,
            IssueDate = dto.IssueDate,
            DueDate = dto.DueDate,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            Currency = dto.Currency,
            TotalAmount = 1234.56m, // static amount
            Status = InvoiceStatus.Draft, // example enum
            Notes = dto.Notes,
            Lines = [.. dto.Lines.Select(line => new InvoiceLineDto
            {
                Description = line.Description,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice
            })]
        };
    }
}

