using IzzyInvoice.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/invoices")]
public class InvoicesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetInvoices([FromQuery] InvoiceQueryDto query)
    {
        return Ok(query);
    }

    [HttpPost]
    public IActionResult CreateInvoices([FromBody] CreateInvoiceDto body)
    {
        return Ok(body);
    }
}