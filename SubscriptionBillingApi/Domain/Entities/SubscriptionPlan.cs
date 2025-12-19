using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Domain.Entities
{
    public class SubscriptionPlan
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public BillingInterval BillingInterval { get; private set; }
        public bool isActive { get; private set; }

        public SubscriptionPlan(string name, decimal price, string currency, BillingInterval billingInterval)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Currency = currency;
            BillingInterval = billingInterval;
            isActive = true;
        }
    }
}
