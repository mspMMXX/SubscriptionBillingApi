using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Enums;

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

        public Task<bool> DeleteAsync(Guid invoiceId)
        { 
            return Task.FromResult(_invoices.Remove(invoiceId));
        }

        public Task<Invoice?> FindDraftAsync(Guid customerId, DateOnly periodStart, DateOnly periodEnd)
        {
            var invoice = _invoices.Values.FirstOrDefault(i =>
            i.CustomerId == customerId &&
            i.PeriodStart == periodStart &&
            i.PeriodEnd == periodEnd &&
            i.Status == InvoiceStatus.Draft);

            return Task.FromResult(invoice);
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

        public Task UpdateAsync(Invoice invoice)
        {
            _invoices[invoice.Id] = invoice;
            return Task.CompletedTask;
        }

        public Task AddLineAsync(InvoiceLine line)
        {
            if (_invoices.TryGetValue(line.InvoiceId, out var invoice))
            {
                invoice.AddLine(line);
            }
            return Task.CompletedTask;
        }
    }
}
