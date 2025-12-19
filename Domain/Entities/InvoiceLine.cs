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
        public decimal LineTotal { get; private set; }
        public InvoiceLine(Guid invoiceId, string description, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            InvoiceId = invoiceId;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            LineTotal = quantity * unitPrice;
        }
    }
}
