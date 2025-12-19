using SubscriptionBillingApi.Domain.Entities;
using System.Runtime.CompilerServices;

namespace SubscriptionBillingApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer?> GetByIdAsync(Guid customerId);
        Task<List<Customer>> GetAllAsync();
        Task DeleteAsync(Guid customerId);
    }
}
