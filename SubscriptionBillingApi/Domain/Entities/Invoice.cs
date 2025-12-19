using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public string InvoiceNumber { get; private set; }
        public Guid CusomerId { get; private set; }
        public DateOnly PeriodStart { get; private set; }
        public DateOnly PeriodEnd { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; }
        public InvoiceStatus Status { get; private set; }

        public Invoice(string invoiceNumber, Guid cusomerId, DateOnly periodStart, DateOnly periodEnd, decimal totalAmount, string currency)
        {
            Id = Guid.NewGuid();
            InvoiceNumber = invoiceNumber;
            CusomerId = cusomerId;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
            IssuedAt = DateTime.UtcNow;
            TotalAmount = totalAmount;
            Currency = currency;
            Status = InvoiceStatus.Draft;
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
