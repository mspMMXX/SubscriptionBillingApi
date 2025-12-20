using SubscriptionBillingApi.Domain.Entities;
using SubscriptionBillingApi.Repositories.Interfaces;

namespace SubscriptionBillingApi.Repositories.InMemory
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {

        private readonly Dictionary<Guid, Customer> _customers = new Dictionary<Guid, Customer>();

        public Task AddAsync(Customer customer)
        {
            _customers[customer.Id] = customer;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid customerId)
        {
            _customers.Remove(customerId);
            return Task.CompletedTask;
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return Task.FromResult(_customers.Values.ToList());
        }

        public Task<Customer?> GetByIdAsync(Guid customerId)
        {
            _customers.TryGetValue(customerId, out var customer);
            return Task.FromResult(customer);
        }
    }
}
