using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.DTOs.Invoices
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid CusomerId { get; set; }
        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd { get; set; }
        public DateTime IssuedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
