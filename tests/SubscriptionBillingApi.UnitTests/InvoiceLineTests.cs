using SubscriptionBillingApi.Domain.Entities;
using Xunit;

namespace SubscriptionBillingApi.UnitTests.Domain;

public class InvoiceLineTests
{
    [Fact]
    public void LineTotal_is_quantity_times_unit_price()
    {
        // Arrange
        var invoiceId = Guid.NewGuid();
        var subscriptionId = Guid.NewGuid();

        var line = new InvoiceLine(
            invoiceId,
            subscriptionId,
            description: "Test line",
            quantity: 3,
            unitPrice: 9.99m);

        // Act
        var total = line.LineTotal;

        // Assert
        Assert.Equal(29.97m, total);
    }
}
