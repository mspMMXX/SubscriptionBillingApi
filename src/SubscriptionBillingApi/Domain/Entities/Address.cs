using SubscriptionBillingApi.Domain.Enums;

namespace SubscriptionBillingApi.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; private set; }
        public Addresstype Addresstype { get; private set; }
        public string HouseNumber { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }

        public Address(Addresstype addresstype, string houseNumber, string street, string city, string zipCode, string country)
        {
            Id = Guid.NewGuid();
            Addresstype = addresstype;
            HouseNumber = houseNumber;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

    }
}
