using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.DTOs.SubscriptionPlans
{
    public class CreateSubscriptionPlanDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public BillingInterval BillingInterval { get; set; }
    }
}
