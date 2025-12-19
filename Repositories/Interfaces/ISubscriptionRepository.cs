using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task AddAsync(Subscription subscription);
        Task<Subscription?> GetByIdAsync(Guid subscriptionId);
        Task<List<Subscription>> GetAllAsync();
        Task DeleteAsync(Guid subscriptionId);
    }
}
