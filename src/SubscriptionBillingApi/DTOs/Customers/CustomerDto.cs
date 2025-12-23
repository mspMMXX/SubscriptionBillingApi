namespace SubscriptionBillingApi.DTOs.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CustomerNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
