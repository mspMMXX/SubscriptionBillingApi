using SubscriptionBillingApi.DTOs.Invoices;

namespace SubscriptionBillingApi.DTOs.Billing
{
    public class BillingDto
    {
        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd { get; set; }
        public List<InvoiceDto> Invoices { get; set; } = new();
    }
}
