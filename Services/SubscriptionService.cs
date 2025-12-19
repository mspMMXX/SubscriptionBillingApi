using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Services
{
    public class SubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task CreateSubscriptionAsync(Subscription subscription)
        {
            await _subscriptionRepository.AddAsync(subscription);
        }

        public async Task<Subscription?> GetSubscriptionByIdAsync(Guid subscriptionId)
        {
            return await _subscriptionRepository.GetByIdAsync(subscriptionId);
        }

        public async Task<List<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _subscriptionRepository.GetAllAsync();
        }

        public async Task DeleteSubscriptionAsync(Guid subscriptionId)
        {
           await _subscriptionRepository.DeleteAsync(subscriptionId);
        }
    }
}
