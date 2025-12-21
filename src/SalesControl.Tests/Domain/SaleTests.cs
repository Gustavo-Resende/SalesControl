using SalesControl.Domain.SaleAggregate;

namespace SalesControl.Tests.Domain
{
    public class SaleTests
    {
        [Fact]
        public void Create_Valid()
        {
            var clientId = Guid.NewGuid();
            var sale = new Sale(clientId);

            var productId = Guid.NewGuid();
            sale.AddItem(productId, 2, 5.5m);

            Assert.NotEqual(Guid.Empty, sale.Id);
            Assert.Equal(11m, sale.Total);
            Assert.Single(sale.Items);
        }

        [Fact]
        public void Create_NoItems_IsEmpty()
        {
            var clientId = Guid.NewGuid();
            var sale = new Sale(clientId);
            Assert.Empty(sale.Items);
        }
    }
}
