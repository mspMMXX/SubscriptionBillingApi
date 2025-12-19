using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Services
{
    public class SubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;

        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        public async Task CreateSubscriptionPlanAsync(SubscriptionPlan subscriptionPlan)
        {
            await _subscriptionPlanRepository.AddAsync(subscriptionPlan);
        }

        public async Task<SubscriptionPlan?> GetSubscriptionPlanByIdAsync(Guid subscriptionPlanId)
        {
            return await _subscriptionPlanRepository.GetByIdAsync(subscriptionPlanId);
        }
        
        public async Task<List<SubscriptionPlan>> GetAllSubscriptionPlansAsync()
        {
            return await _subscriptionPlanRepository.GetAllAsync();
        }

        public async Task DeleteSubscriptionPlanAsync(Guid subscriptionPlanId)
        {
            await _subscriptionPlanRepository.DeleteAsync(subscriptionPlanId);
        }
    }
}
