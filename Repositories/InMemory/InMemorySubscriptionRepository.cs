using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.InMemory
{
    public class InMemorySubscriptionRepository : ISubscriptionRepository
    {
        private readonly Dictionary<Guid, Subscription> _subscriptions = new Dictionary<Guid, Subscription>();

        public Task AddAsync(Subscription subscription)
        {
            _subscriptions[subscription.Id] = subscription;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid subscriptionId)
        {
            _subscriptions.Remove(subscriptionId);
            return Task.CompletedTask;
        }

        public Task<List<Subscription>> GetAllAsync()
        {
            return Task.FromResult(_subscriptions.Values.ToList());
        }

        public Task<Subscription?> GetByIdAsync(Guid subscriptionId)
        {
            _subscriptions.TryGetValue(subscriptionId, out var subscription);
            return Task.FromResult(subscription);
        }
    }
}
