using InvoiceGenerator.Web.Models.Dtos.Invoices;
using InvoiceGenerator.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController] // Enables automatic model validation & API behaviors
[Route("api/invoices")] // Base route for all actions in this controller
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    /// Constructor injection is how ASP.NET Core provides dependencies.
    /// The DI container will inject an implementation of IInvoiceService.
    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost]
    public ActionResult<InvoiceDto> CreateInvoice(
        [FromBody] CreateInvoiceRequestDto dto)
    {
        // Controller does NOT contain business logic
        // It simply delegates the work
        var result = _invoiceService.CreateInvoice(dto);

        // Returns HTTP 201 Created with the response DTO
        return CreatedAtAction(
            nameof(CreateInvoice),
            result
        );
    }
}