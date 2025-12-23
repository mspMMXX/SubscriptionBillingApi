using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Services
{
    /// <summary>
    /// Application service for subscription plan–related use cases.
    /// Provides a thin abstraction over the subscription plan repository.
    /// </summary>
    public class SubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;

        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        /// <summary>
        /// Creates a new subscription plan.
        /// </summary>
        public async Task CreateSubscriptionPlanAsync(SubscriptionPlan subscriptionPlan)
        {
            await _subscriptionPlanRepository.AddAsync(subscriptionPlan);
        }


        /// <summary>
        /// Returns a subscription plan by id, or null if it does not exist.
        /// </summary>
        public async Task<SubscriptionPlan?> GetSubscriptionPlanByIdAsync(Guid subscriptionPlanId)
        {
            return await _subscriptionPlanRepository.GetByIdAsync(subscriptionPlanId);
        }

        /// <summary>
        /// Returns all subscription plans.
        /// </summary>
        public async Task<List<SubscriptionPlan>> GetAllSubscriptionPlansAsync()
        {
            return await _subscriptionPlanRepository.GetAllAsync();
        }

        /// <summary>
        /// Deletes a subscription plan by id.
        /// </summary>
        public async Task DeleteSubscriptionPlanAsync(Guid subscriptionPlanId)
        {
            await _subscriptionPlanRepository.DeleteAsync(subscriptionPlanId);
        }
    }
}
