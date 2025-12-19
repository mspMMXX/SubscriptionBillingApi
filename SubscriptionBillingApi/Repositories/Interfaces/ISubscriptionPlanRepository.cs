using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Repositories.Interfaces
{
    public interface ISubscriptionPlanRepository
    {
        Task AddAsync(SubscriptionPlan plan);
        Task<SubscriptionPlan?> GetByIdAsync(Guid planId);
        Task<List<SubscriptionPlan>> GetAllAsync();
        Task DeleteAsync(Guid planId);
    }
}
