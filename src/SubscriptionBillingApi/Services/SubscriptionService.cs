using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Services
{
    /// <summary>
    /// Application service for subscription-related use cases.
    /// Acts as a thin layer between controllers and the subscription repository.
    /// </summary>
    public class SubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        /// <summary>
        /// Creates a new subscription.
        /// </summary>
        public async Task CreateSubscriptionAsync(Subscription subscription)
        {
            await _subscriptionRepository.AddAsync(subscription);
        }

        /// <summary>
        /// Returns a subscription by id, or null if it does not exist.
        /// </summary>
        public async Task<Subscription?> GetSubscriptionByIdAsync(Guid subscriptionId)
        {
            return await _subscriptionRepository.GetByIdAsync(subscriptionId);
        }

        /// <summary>
        /// Returns all subscriptions.
        /// </summary>
        public async Task<List<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _subscriptionRepository.GetAllAsync();
        }


        /// <summary>
        /// Deletes a subscription by id.
        /// </summary>
        public async Task DeleteSubscriptionAsync(Guid subscriptionId)
        {
           await _subscriptionRepository.DeleteAsync(subscriptionId);
        }
    }
}
