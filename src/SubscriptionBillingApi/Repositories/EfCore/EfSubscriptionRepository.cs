using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    /// <summary>
    /// EF Core–based repository implementation for Subscription entities.
    /// Provides persistence and query logic for subscriptions.
    /// </summary>
    public class EfSubscriptionRepository : ISubscriptionRepository
    {
        private readonly BillingDbContext _db;
        public EfSubscriptionRepository(BillingDbContext db) => _db = db;

        /// <summary>
        /// Adds a new subscription to the database.
        /// </summary>
        public async Task AddAsync(Subscription subscription)
        {
            _db.Subscriptions.Add(subscription);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a subscription by id, or null if it does not exist.
        /// </summary>
        public Task<Subscription?> GetByIdAsync(Guid subscriptionId)
            => _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId);

        /// <summary>
        /// Returns all subscriptions.
        /// </summary>
        public Task<List<Subscription>> GetAllAsync()
            => _db.Subscriptions.ToListAsync();

        /// <summary>
        /// Deletes a subscription by id.
        /// If the subscription does not exist, the operation is silently ignored.
        /// </summary>
        public async Task DeleteAsync(Guid subscriptionId)
        {
            var entity = await _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId);
            if (entity is null) return;

            _db.Subscriptions.Remove(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Returns all subscriptions that are due for billing within the given period.
        /// A subscription is considered "due" if it is active and overlaps
        /// with the billing period.
        /// </summary>
        public Task<List<Subscription>> GetDueSubscriptionsAsync(DateOnly periodStart, DateOnly periodEnd)
        {
            // Einfach & stabil für deine Abgabe:
            // "Due" = Active und überlappt den Billing-Zeitraum
            return _db.Subscriptions
                .Where(s =>
                    s.Status == SubscriptionStatus.Active &&
                    s.StartDate <= periodEnd &&
                    (s.EndDate == null || s.EndDate.Value >= periodStart))
                .ToListAsync();
        }
    }
}
