using SubscriptionBillingApi.Repositories.Interfaces;
using SubscriptionBillingApi.Domain.Entities;

namespace SubscriptionBillingApi.Services
{
    /// <summary>
    /// Application service for customer-related use cases.
    /// Acts as a thin layer between controllers and repositories.
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        /// <summary>
        /// Creates a new customer.
        /// </summary>
        public async Task CreateCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        /// <summary>
        /// Returns a customer by id, or null if it does not exist.
        /// </summary>
        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        /// <summary>
        /// Returns all customers.
        /// </summary>
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        /// <summary>
        /// Deletes a customer by id.
        /// </summary>
        public async Task DeleteCustomerAsync(Guid customerId)
        {
            await _customerRepository.DeleteAsync(customerId);
        }
    }
}
