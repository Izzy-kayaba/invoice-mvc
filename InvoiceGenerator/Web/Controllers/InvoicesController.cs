using InvoiceGenerator.Services.Interfaces;
using InvoiceGenerator.Web.Models.Dtos.Invoices;
using InvoiceGenerator.Models.Dtos.Invoices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    private Guid GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InvoiceResponseDto>> GetInvoice(Guid id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id, GetUserId()).ConfigureAwait(true);

        if (invoice == null)
            return NotFound();

        return Ok(invoice);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteInvoice(Guid id)
    {
        var deletedInvoice = await _invoiceService.DeleteInvoiceAsync(id, GetUserId()).ConfigureAwait(true);

        if (!deletedInvoice)
            return NotFound();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceResponseDto>>> GetInvoices()
    {
        var invoices = await _invoiceService.GetAllAsync(GetUserId()).ConfigureAwait(true);

        if (!invoices.Any())
            return NotFound();

        return Ok(invoices);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceResponseDto>> CreateInvoice(
        [FromBody] CreateInvoiceRequestDto dto)
    {
        var result = await _invoiceService.CreateInvoiceAsync(dto, GetUserId()).ConfigureAwait(true);

        // Return the action name that returns a single invoice.
        return CreatedAtAction(
            nameof(GetInvoice),
            new { id = result.Id },
            result
        );
    }
}