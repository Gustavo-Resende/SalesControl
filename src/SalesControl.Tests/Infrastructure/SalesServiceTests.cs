using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SalesControl.Infrastructure.Persistence;
using SalesControl.Infrastructure.Services;
using SalesControl.Application.Sales.DTOs;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace SalesControl.Tests.Infrastructure
{
    public class SalesServiceTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly AppDbContext _db;
        private readonly SalesService _service;

        public SalesServiceTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            _db = new AppDbContext(options);
            _db.Database.EnsureCreated();

            // seed minimal data: clients and products
            var client = new SalesControl.Domain.ClientAggregate.Client("Test", "test@example.com", "123");
            _db.Clients.Add(client);

            var p1 = new SalesControl.Domain.ProductAggregate.Product("P1", 10m, 5);
            var p2 = new SalesControl.Domain.ProductAggregate.Product("P2", 20m, 1);
            _db.Products.AddRange(p1, p2);

            _db.SaveChanges();

            _service = new SalesService(_db);
        }

        [Fact]
        public async Task RegisterSale_Success_ReducesStockAndCreatesSale()
        {
            var clientId = _db.Clients.Select(c => c.Id).First();

            var dto = new RegisterSaleDto(clientId, new[] { new RegisterSaleItemDto(_db.Products.First().Id, 2) });

            var saleId = await _service.RegisterSaleAsync(dto);

            Assert.NotEqual(Guid.Empty, saleId);

            var prod = await _db.Products.FindAsync(_db.Products.First().Id);
            Assert.Equal(3, prod!.Stock);

            var sale = await _db.Sales.FindAsync(saleId);
            Assert.NotNull(sale);
        }

        [Fact]
        public async Task RegisterSale_Fails_InsufficientStock_RollsBack()
        {
            var clientId = _db.Clients.Select(c => c.Id).First();
            var product = _db.Products.First(p => p.Stock == 1);

            var dto = new RegisterSaleDto(clientId, new[] { new RegisterSaleItemDto(product.Id, 2) });

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterSaleAsync(dto));

            // ensure stock unchanged
            var prod = await _db.Products.FindAsync(product.Id);
            Assert.Equal(1, prod!.Stock);

            // no sales created
            Assert.Empty(_db.Sales.ToList());
        }

        public void Dispose()
        {
            _db.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
    }
}
