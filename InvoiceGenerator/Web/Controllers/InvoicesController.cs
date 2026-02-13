using InvoiceGenerator.Services.Interfaces;
using InvoiceGenerator.Web.Models.Dtos.Invoices;
using InvoiceGenerator.Models.Dtos.Invoices;

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
    public async Task<ActionResult<InvoiceResponseDto>> CreateInvoice(
        [FromBody] CreateInvoiceRequestDto dto)
    {
        var result = await _invoiceService.CreateInvoiceAsync(dto);

        // Return 201 Created. Replace "GetInvoice" with the action name that returns a single invoice.
        return CreatedAtAction(
            nameof(GetInvoice),
            new { id = result.Id },
            result
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InvoiceResponseDto>> GetInvoice(Guid id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);

        if (invoice == null)
            return NotFound();

        return Ok(invoice);
    }
}