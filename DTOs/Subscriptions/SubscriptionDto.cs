using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.DTOs.Subscriptions
{
    public class SubscriptionDto
    {   
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PlanId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateOnly NextBillingDate { get; set; }
        public DateOnly? CancelDate { get;set; }
    }
}
