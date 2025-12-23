using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.InMemory
{
    public class InMemorySubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly Dictionary<Guid, SubscriptionPlan> _subscriptionPlans = new Dictionary<Guid, SubscriptionPlan>();

        public Task AddAsync(SubscriptionPlan plan)
        {
            _subscriptionPlans[plan.Id] = plan;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid planId)
        {
            _subscriptionPlans.Remove(planId);
            return Task.CompletedTask;
        }

        public Task<List<SubscriptionPlan>> GetAllAsync()
        {
            return Task.FromResult(_subscriptionPlans.Values.ToList());
        }

        public Task<SubscriptionPlan?> GetByIdAsync(Guid planId)
        {
            _subscriptionPlans.TryGetValue(planId, out var plan);
            return Task.FromResult(plan);
        }
    }
}
