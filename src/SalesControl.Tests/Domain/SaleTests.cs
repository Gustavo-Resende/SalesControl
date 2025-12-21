using SalesControl.Domain.SaleAggregate;
using SalesControl.Domain.SaleItemAggregate;

namespace SalesControl.Tests.Domain
{
    public class SaleTests
    {
        [Fact]
        public void Create_Valid()
        {
            var item = new SaleItem(Guid.NewGuid(), 2, 5.5m);
            var sale = new Sale(Guid.NewGuid(), new List<SaleItem> { item });

            Assert.NotEqual(Guid.Empty, sale.Id);
            Assert.Equal(11m, sale.Total);
            Assert.Single(sale.Items);
        }

        [Fact]
        public void Create_NoItems_Throws()
        {
            Assert.ThrowsAny<ArgumentException>(() => new Sale(Guid.NewGuid(), new List<SaleItem>()));
        }
    }
}
