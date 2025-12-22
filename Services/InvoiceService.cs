using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Services
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task CreateInvoiceAsync(Invoice invoice)
        {
            var invoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            invoice.AssignInvoiceNumber(invoiceNumber);
            invoice.AssignIssuedAt(DateTime.UtcNow);

            await _invoiceRepository.AddAsync(invoice);
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId)
        {
            return await _invoiceRepository.GetByIdAsync(invoiceId);
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllAsync();
        }

        public async Task<bool> DeleteInvoiceAsync(Guid invoiceId)
        {
            return await _invoiceRepository.DeleteAsync(invoiceId);
        }
    }
}
