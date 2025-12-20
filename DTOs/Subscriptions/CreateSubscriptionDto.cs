using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.DTOs.Subscriptions
{
    public class CreateSubscriptionDto
    {
        public Guid CustomerId { get; set; }
        public Guid PlanId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
