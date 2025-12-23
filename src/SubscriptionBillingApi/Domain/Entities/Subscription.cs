using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid PlanId { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly? EndDate { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public DateOnly NextBillingDate { get; private set; }
        public DateOnly? CancelDate { get; private set; }

        public Subscription(Guid customerId, Guid planId, DateOnly startDate)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            PlanId = planId;
            StartDate = startDate;
            Status = SubscriptionStatus.Active;
        }
    }
}
