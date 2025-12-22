using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(Guid invoiceId);
        Task<List<Invoice>> GetAllAsync();
        Task<bool> DeleteAsync(Guid invoiceId);
        Task<Invoice?> FindDraftAsync(Guid customerId, DateOnly periodStart, DateOnly periodEnd);
        Task UpdateAsync(Invoice invoice);
        Task AddLineAsync(InvoiceLine line);


    }
}
