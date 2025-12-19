using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(Guid invoiceId);
        Task<List<Invoice>> GetAllAsync();
        Task DeleteAsync(Guid invoiceId);
    }
}
