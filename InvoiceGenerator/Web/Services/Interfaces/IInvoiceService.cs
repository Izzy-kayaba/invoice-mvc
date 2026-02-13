using InvoiceGenerator.Web.Models.Dtos.Invoices;

namespace InvoiceGenerator.Web.Services.Interfaces;

/// Defines invoice-related business operations.
/// Controllers depend on this interface, not implementation.
public interface IInvoiceService
{
    /// Creates an invoice using provided data.
    /// Business logic lives here.
    InvoiceDto CreateInvoice(CreateInvoiceRequestDto dto);
}

