using SalesControl.Domain.ProductAggregate;

namespace SalesControl.Tests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Create_Valid()
        {
            var product = new Product("Produto Gustavo", 9.99m, 10, "desc");

            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal("Produto Gustavo", product.Name);
            Assert.Equal(9.99m, product.Price);
            Assert.Equal(10, product.Stock);
            Assert.Equal("desc", product.Description);
        }

        [Fact]
        public void Update_Price()
        {
            var product = new Product("Produto Gustavo", 9.99m, 10);
            product.UpdatePrice(19.99m);
            Assert.Equal(19.99m, product.Price);
        }

        [Fact]
        public void Stock_Adjust()
        {
            var product = new Product("Produto Gustavo", 9.99m, 10);
            product.IncreaseStock(5);
            Assert.Equal(15, product.Stock);
            product.DecreaseStock(3);
            Assert.Equal(12, product.Stock);
        }

        [Fact]
        public void Stock_Insufficient_Throws()
        {
            var product = new Product("Produto Gustavo", 9.99m, 2);
            Assert.Throws<InvalidOperationException>(() => product.DecreaseStock(3));
        }
    }
}
