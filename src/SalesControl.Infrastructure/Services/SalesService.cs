using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using SalesControl.Domain.SaleAggregate;
using SalesControl.Infrastructure.Persistence;

namespace SalesControl.Infrastructure.Services
{
    public class SalesService : ISalesService
    {
        private readonly AppDbContext _db;

        public SalesService(AppDbContext db) => _db = db;

        public async Task<Guid> RegisterSaleAsync(RegisterSaleDto dto, CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(dto, nameof(dto));
            Guard.Against.Default(dto.ClientId, nameof(dto.ClientId));
            Guard.Against.NullOrEmpty(dto.Items, nameof(dto.Items));

            var client = await _db.Clients.FindAsync(dto.ClientId, cancellationToken)
                         ?? throw new InvalidOperationException("Cliente não encontrado.");

            var productIds = dto.Items.Select(i => i.ProductId).Distinct().ToArray();

            await using var tx = await _db.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var products = await _db.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                foreach (var item in dto.Items)
                {
                    var product = products.SingleOrDefault(p => p.Id == item.ProductId)
                                  ?? throw new InvalidOperationException($"Produto {item.ProductId} não encontrado.");

                    if (item.Quantity <= 0)
                        throw new InvalidOperationException("Quantidade inválida.");

                    if (item.Quantity > product.Stock)
                        throw new InvalidOperationException($"Estoque insuficiente para o produto '{product.Name}'.");
                }

                foreach (var item in dto.Items)
                {
                    var product = products.Single(p => p.Id == item.ProductId);
                    product.DecreaseStock(item.Quantity);
                    _db.Products.Update(product);
                }

                var sale = new Sale(dto.ClientId);
                foreach (var item in dto.Items)
                {
                    var product = products.Single(p => p.Id == item.ProductId);
                    sale.AddItem(product.Id, item.Quantity, product.Price);
                }

                await _db.Sales.AddAsync(sale, cancellationToken);

                await _db.SaveChangesAsync(cancellationToken);
                await tx.CommitAsync(cancellationToken);

                return sale.Id;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}