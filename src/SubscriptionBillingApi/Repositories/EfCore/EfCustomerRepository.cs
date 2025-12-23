using Microsoft.EntityFrameworkCore;
using SubscriptionBillingApi.Data;
using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.EfCore
{
    /// <summary>
    /// EF Core–based repository implementation for Customer entities.
    /// Handles persistence concerns and hides EF Core specifics
    /// behind the repository interface.
    /// </summary>
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly BillingDbContext _db;
        public EfCustomerRepository(BillingDbContext db) => _db = db;

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        public async Task AddAsync(Customer customer)
        {
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a customer by id, or null if it does not exist.
        /// </summary>
        public Task<Customer?> GetByIdAsync(Guid customerId)
            => _db.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

        /// <summary>
        /// Returns all customers.
        /// </summary>
        public Task<List<Customer>> GetAllAsync()
            => _db.Customers.ToListAsync();

        /// <summary>
        /// Deletes a customer by id.
        /// If the customer does not exist, the operation is silently ignored.
        /// </summary>
        public async Task DeleteAsync(Guid customerId)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
            if (entity is null) return;

            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
