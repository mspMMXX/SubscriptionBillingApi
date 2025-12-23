using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.DTOs.SubscriptionPlans
{
    public class SubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public BillingInterval BillingInterval { get; set; }
        public bool IsActive { get; set; }
    }
}
