using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.DTOs.Invoices
{
    public class CreateInvoiceDto
    {
        public Guid CustomerId { get; set; }
    }
}
