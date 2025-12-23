using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    /// <summary>
    /// EF Core–based repository implementation for SubscriptionPlan entities.
    /// Handles persistence and retrieval of subscription plans.
    /// </summary>
    public class EfSubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly BillingDbContext _db;
        public EfSubscriptionPlanRepository(BillingDbContext db) => _db = db;

        /// <summary>
        /// Adds a new subscription plan to the database.
        /// </summary>
        public async Task AddAsync(SubscriptionPlan plan)
        {
            _db.SubscriptionPlans.Add(plan);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a subscription plan by id, or null if it does not exist.
        /// </summary>
        public Task<SubscriptionPlan?> GetByIdAsync(Guid planId)
            => _db.SubscriptionPlans.FirstOrDefaultAsync(x => x.Id == planId);

        /// <summary>
        /// Returns all subscription plans.
        /// </summary>
        public Task<List<SubscriptionPlan>> GetAllAsync()
            => _db.SubscriptionPlans.ToListAsync();

        /// <summary>
        /// Deletes a subscription plan by id.
        /// If the plan does not exist, the operation is silently ignored.
        /// </summary>
        public async Task DeleteAsync(Guid planId)
        {
            var entity = await _db.SubscriptionPlans.FirstOrDefaultAsync(x => x.Id == planId);
            if (entity is null) return;

            _db.SubscriptionPlans.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
