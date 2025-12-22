using Microsoft.AspNetCore.Mvc;
using SubscriptionBillingApi.Services;
using SubscriptionBillingApi.DTOs.Billing;

namespace SubscriptionBillingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly BillingService _billingService;

        public BillingController(BillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpPost("run")]
        public async Task<ActionResult<List<BillingDto>>> Run([FromBody] CreateBillingDto dto)
        {
            var invoices = await _billingService.RunBilling(dto.PeriodStart, dto.PeriodEnd);

            return Ok(new BillingDto
            {
                PeriodStart = dto.PeriodStart,
                PeriodEnd = dto.PeriodEnd,
                Invoices = invoices.Select(InvoicesController.MapToDto).ToList()
            });
        }
    }
}
