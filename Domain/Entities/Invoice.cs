using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Domain.Entities
{
    public class Invoice
    {
        private readonly List<InvoiceLine> _lines = new();
        public IReadOnlyCollection<InvoiceLine> Lines => _lines.AsReadOnly();

        public Guid Id { get; private set; }
        public string InvoiceNumber { get; private set; } = string.Empty;
        public Guid CustomerId { get; private set; }
        public DateOnly PeriodStart { get; private set; }
        public DateOnly PeriodEnd { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; } = string.Empty;
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

        public void AssignInvoiceNumber(string invoiceNumber)
        {
            if (!string.IsNullOrWhiteSpace(InvoiceNumber))
                throw new InvalidOperationException("InvoiceNumber already assigned");

            InvoiceNumber = invoiceNumber;
        }

        public void AssignIssuedAt(DateTime issuedAt)
        {
            if (IssuedAt != default)
                throw new InvalidOperationException("IssuedAt already assigned.");
            IssuedAt = issuedAt;
        }

        public void AddLine (InvoiceLine line)
        {
            if (Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Cannot add lines unless invoice is Draft.");

            _lines.Add(line);
            TotalAmount = CalculateTotal();
        }

        private decimal CalculateTotal() => _lines.Sum(l => l.LineTotal);
    }
}
