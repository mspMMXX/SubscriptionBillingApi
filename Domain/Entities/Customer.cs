using System.Data;

namespace SubscriptionBillingApi.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string CustomerNumber { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Customer(string customerNumber, string lastName, string firstName, string phone, string email)
        {
            Id = Guid.NewGuid();
            CustomerNumber = customerNumber;
            LastName = lastName;
            FirstName = firstName;
            Phone = phone;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }

        public Subscription CreateSubscription(Guid planId, DateOnly startDate)
        {
            // Implementation for creating a new subscription for the customer
            throw new NotImplementedException();

        }
    }
}
