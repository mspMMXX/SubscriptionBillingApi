using Microsoft.AspNetCore.Mvc;
using SubscriptionBillingApi.DTOs.Invoices;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;
using SubscriptionBillingApi.Services;

namespace SubscriptionBillingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoicesController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> Create([FromBody] CreateInvoiceDto dto)
        {
            var invoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd-HHmmss}";
            var issuedAt = DateTime.UtcNow;

            var invoice = new Invoice(
                invoiceNumber: invoiceNumber,
                customerId: dto.CustomerId,
                periodStart: dto.PeriodStart,
                periodEnd: dto.PeriodEnd,
                issuedAt: issuedAt,
                currency: dto.Currency,
                status: InvoiceStatus.Draft
            );

            await _invoiceService.CreateInvoiceAsync(invoice);
            var responseDto = MapToDto(invoice);

            return CreatedAtAction(
                nameof(GetById),
                new { id = invoice.Id },
                responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceDto>>> GetAll()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            var dtos = invoices.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InvoiceDto>> GetById([FromRoute(Name = "id")] Guid invoiceId)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
            if (invoice is null)
                return NotFound();

            return Ok(MapToDto(invoice));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid invoiceId)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
            if (invoice is null)
                return NotFound();

            await _invoiceService.DeleteInvoiceAsync(invoiceId);
            return NoContent();
        }

        public static InvoiceDto MapToDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerId = invoice.CustomerId,
                PeriodStart = invoice.PeriodStart,
                PeriodEnd = invoice.PeriodEnd,
                IssuedAt = invoice.IssuedAt,
                TotalAmount = invoice.TotalAmount,
                Currency = invoice.Currency,
                Status = invoice.Status
            };
        }
    }
}
