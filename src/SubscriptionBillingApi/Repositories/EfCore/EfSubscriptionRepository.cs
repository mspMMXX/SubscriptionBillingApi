using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Domain.Enums;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    public class EfSubscriptionRepository : ISubscriptionRepository
    {
        private readonly BillingDbContext _db;
        public EfSubscriptionRepository(BillingDbContext db) => _db = db;

        public async Task AddAsync(Subscription subscription)
        {
            _db.Subscriptions.Add(subscription);
            await _db.SaveChangesAsync();
        }

        public Task<Subscription?> GetByIdAsync(Guid subscriptionId)
            => _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId);

        public Task<List<Subscription>> GetAllAsync()
            => _db.Subscriptions.ToListAsync();

        public async Task DeleteAsync(Guid subscriptionId)
        {
            var entity = await _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId);
            if (entity is null) return;

            _db.Subscriptions.Remove(entity);
            await _db.SaveChangesAsync();
        }

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
