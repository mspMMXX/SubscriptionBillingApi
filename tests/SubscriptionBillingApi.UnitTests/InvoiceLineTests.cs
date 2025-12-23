using SubscriptionBillingApi.Domain.Entities;
using Xunit;

namespace SubscriptionBillingApi.UnitTests;

public class InvoiceLineTests
{
    [Fact]
    public void LineTotal_IsQuantityTimesUnitPrice()
    {
        var invoiceId = Guid.NewGuid();
        var subscriptionId = Guid.NewGuid();

        var line = new InvoiceLine(invoiceId, subscriptionId, "Test", 3, 9.99m);

        Assert.Equal(29.97m, line.LineTotal);
    }

    [Fact]
    public void Ctor_Throws_WhenQuantityIsZeroOrLess()
    {
        var invoiceId = Guid.NewGuid();
        var subscriptionId = Guid.NewGuid();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new InvoiceLine(invoiceId, subscriptionId, "Test", 0, 1m));
    }

    [Fact]
    public void Ctor_Throws_WhenDescriptionIsMissing()
    {
        var invoiceId = Guid.NewGuid();
        var subscriptionId = Guid.NewGuid();

        Assert.Throws<ArgumentException>(() =>
            new InvoiceLine(invoiceId, subscriptionId, "", 1, 1m));
    }
}
