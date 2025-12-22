using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.DTOs.Invoices
{
    public class CreateInvoiceDto
    {
        public Guid CustomerId { get; set; }
        public string InvoiceNUmber { get; set; }
        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd { get; set; }
        public string Currency { get; set; }
    }
}
