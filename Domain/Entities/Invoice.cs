using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public string InvoiceNumber { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateOnly PeriodStart { get; private set; }
        public DateOnly PeriodEnd { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; }
        public InvoiceStatus Status { get; private set; }

        public Invoice(string invoiceNumber, Guid customerId, DateOnly periodStart, DateOnly periodEnd, DateTime issuedAt, string currency, InvoiceStatus status)
        {
            Id = Guid.NewGuid();
            InvoiceNumber = invoiceNumber;
            CustomerId = customerId;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
            IssuedAt = issuedAt;
            Currency = currency;
            Status = status;
        }

        public void AddLine (InvoiceLine line)
        {
            // Implementation for adding an invoice line to the invoice
        }

        public decimal CalculateTotal()
        {
            // Implementation for calculating the total amount of the invoice
            throw new NotImplementedException();

        }
    }
}
