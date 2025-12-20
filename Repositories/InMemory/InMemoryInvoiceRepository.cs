using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.InMemory
{
    public class InMemoryInvoiceRepository : IInvoiceRepository
    {
        private readonly Dictionary<Guid, Invoice> _invoices = new Dictionary<Guid, Invoice>();

        public Task AddAsync(Invoice invoice)
        {
            _invoices[invoice.Id] = invoice;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid invoiceId)
        {
            _invoices.Remove(invoiceId);
            return Task.CompletedTask;
        }

        public Task<List<Invoice>> GetAllAsync()
        {
            return Task.FromResult(_invoices.Values.ToList());
        }

        public Task<Invoice?> GetByIdAsync(Guid invoiceId)
        {
            _invoices.TryGetValue(invoiceId, out var invoice);
            return Task.FromResult(invoice);
        }
    }
}
