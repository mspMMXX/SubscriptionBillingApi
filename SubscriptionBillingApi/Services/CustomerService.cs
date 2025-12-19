using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task DeleteCustomerAsync(Guid customerId)
        {
            await _customerRepository.DeleteAsync(customerId);
        }
    }
}
