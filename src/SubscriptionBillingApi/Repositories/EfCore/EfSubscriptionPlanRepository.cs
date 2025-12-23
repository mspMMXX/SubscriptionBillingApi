using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    public class EfSubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly BillingDbContext _db;
        public EfSubscriptionPlanRepository(BillingDbContext db) => _db = db;

        public async Task AddAsync(SubscriptionPlan plan)
        {
            _db.SubscriptionPlans.Add(plan);
            await _db.SaveChangesAsync();
        }

        public Task<SubscriptionPlan?> GetByIdAsync(Guid planId)
            => _db.SubscriptionPlans.FirstOrDefaultAsync(x => x.Id == planId);

        public Task<List<SubscriptionPlan>> GetAllAsync()
            => _db.SubscriptionPlans.ToListAsync();

        public async Task DeleteAsync(Guid planId)
        {
            var entity = await _db.SubscriptionPlans.FirstOrDefaultAsync(x => x.Id == planId);
            if (entity is null) return;

            _db.SubscriptionPlans.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
