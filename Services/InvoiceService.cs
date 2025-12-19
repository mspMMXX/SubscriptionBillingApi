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

        public async Task DeleteInvoiceAsync(Guid invoiceId)
        {
            await _invoiceRepository.DeleteAsync(invoiceId);
        }
    }
}
