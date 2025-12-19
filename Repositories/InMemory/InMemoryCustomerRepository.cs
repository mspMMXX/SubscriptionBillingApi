using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.InMemory
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {

        private readonly Dictionary<Guid, Customer> _customer = new();

        public Task AddAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetByIdAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
