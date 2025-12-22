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

        public Invoice(Guid customerId, DateOnly periodStart, DateOnly periodEnd, string currency, InvoiceStatus status)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
            Currency = currency;
            Status = status;
        }

        public void AssignInvoiceNumber(string invoicNumber)
        {
            if (!string.IsNullOrWhiteSpace(invoicNumber))
                throw new InvalidOperationException("InvoiceNumber already assigned");

            InvoiceNumber = invoicNumber;
        }

        public void AssignIssuedAt(DateTime issuedAt)
        {
            if (IssuedAt != default)
                throw new InvalidOperationException("IssuedAt already assigned.");
            IssuedAt = issuedAt;
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
