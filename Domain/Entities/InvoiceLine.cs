namespace SubscriptionBillingApi.Domain.Entities
{
    public class InvoiceLine
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public Guid SubscriptionId { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal LineTotal => Quantity * UnitPrice;
        public InvoiceLine(Guid invoiceId, Guid subscriptionId, string description, int quantity, decimal unitPrice)
        {
            if (invoiceId == Guid.Empty)
                throw new ArgumentException("InvoiceId is required.", nameof(invoiceId));

            if (subscriptionId == Guid.Empty)
                throw new ArgumentException("SubscriptionId is required.", nameof(subscriptionId));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be > 0.");

            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice must be >= 0.");

            Id = Guid.NewGuid();
            InvoiceId = invoiceId;
            SubscriptionId = subscriptionId;
            Description = description.Trim();
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
