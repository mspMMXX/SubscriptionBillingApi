using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly BillingDbContext _db;
        public EfCustomerRepository(BillingDbContext db) => _db = db;

        public async Task AddAsync(Customer customer)
        {
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
        }

        public Task<Customer?> GetByIdAsync(Guid customerId)
            => _db.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

        public Task<List<Customer>> GetAllAsync()
            => _db.Customers.ToListAsync();

        public async Task DeleteAsync(Guid customerId)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
            if (entity is null) return;

            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
