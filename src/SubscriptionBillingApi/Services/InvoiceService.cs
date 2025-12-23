using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Services
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Invoice> GetOrCreateDraftInvoice(Guid customerId, DateOnly periodStart, DateOnly periodEnd, string currency)
        {
            var existing = await _invoiceRepository.FindDraftAsync(customerId, periodStart, periodEnd);
            if (existing is not null) return existing;

            var invoice = new Invoice(customerId, periodStart, periodEnd, currency, InvoiceStatus.Draft);

            var invoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

            invoice.AssignInvoiceNumber(invoiceNumber);
            invoice.AssignIssuedAt(DateTime.UtcNow);

            await _invoiceRepository.AddAsync(invoice);
            return invoice;
        }

        public Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId)
        {
            return _invoiceRepository.GetByIdAsync(invoiceId);
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllAsync();
        }

        public async Task<bool> DeleteInvoiceAsync(Guid invoiceId)
        {
            return await _invoiceRepository.DeleteAsync(invoiceId);
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            await _invoiceRepository.UpdateAsync(invoice);
        }

        public Task AddLineAsync(InvoiceLine line) => _invoiceRepository.AddLineAsync(line);
    }
}
