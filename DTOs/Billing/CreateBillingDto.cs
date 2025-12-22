namespace SubscriptionBillingApi.DTOs.Billing
{
    public class CreateBillingDto
    {
        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd { get; set; }
    }
}
