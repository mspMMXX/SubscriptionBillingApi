namespace SubscriptionBillingApi.DTOs.Customers
{
    public class CreateCustomerDto
    {
        public string CustomerNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
