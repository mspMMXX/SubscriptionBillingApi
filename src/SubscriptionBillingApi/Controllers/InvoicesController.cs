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
            var deleted = await _invoiceService.DeleteInvoiceAsync(invoiceId);
            if (!deleted) return NotFound();
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
