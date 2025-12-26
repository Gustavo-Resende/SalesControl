using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using SalesControl.Application.Interfaces;
using SalesControl.Application.Sales.DTOs;
using SalesControl.Domain.SaleAggregate;
using SalesControl.Infrastructure.Persistence;
using System.Linq;

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
                // Save stock updates together with the sale in a single SaveChanges call to improve performance
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
            catch (InvalidOperationException ex)
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync(cancellationToken);
                throw new Exception("Erro inesperado ao registrar venda.", ex);
            }
        }

        public async Task<SaleDetailDto?> GetSaleByIdAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            if (saleId == Guid.Empty) return null;

            // Query sale and its items with product data
            var sale = await _db.Sales
                .AsNoTracking()
                .Where(s => s.Id == saleId)
                .Select(s => new
                {
                    s.Id,
                    s.ClientId,
                    s.CreatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (sale is null) return null;

            var items = await _db.SaleItems
                .AsNoTracking()
                .Where(si => EF.Property<Guid>(si, "sale_id") == saleId)
                .Join(_db.Products.AsNoTracking(),
                      si => si.ProductId,
                      p => p.Id,
                      (si, p) => new SaleItemDetailDto(p.Id, p.Name, si.Quantity, si.UnitPrice, si.Quantity * si.UnitPrice))
                .ToListAsync(cancellationToken);

            var total = items.Sum(i => i.LineTotal);

            return new SaleDetailDto(sale.Id, sale.ClientId, sale.CreatedAt, items, total);
        }

        public async Task<System.Collections.Generic.List<SaleReportRowDto>> GetSalesReportAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default)
        {
            // Normalize dates and convert to UTC DateTimeOffset (Postgres timestamptz expects UTC offset)
            var startDt = new DateTimeOffset(start.Date.ToUniversalTime(), TimeSpan.Zero);
            var endDt = new DateTimeOffset(end.Date.AddDays(1).AddTicks(-1).ToUniversalTime(), TimeSpan.Zero);

            // Query sales with items and join client/product info using single query (avoid nested queries that can cause concurrent DbContext usage)
            var rows = await (from s in _db.Sales.AsNoTracking()
                              join si in _db.SaleItems.AsNoTracking() on s.Id equals EF.Property<Guid>(si, "sale_id")
                              join p in _db.Products.AsNoTracking() on si.ProductId equals p.Id
                              join c in _db.Clients.AsNoTracking() on s.ClientId equals c.Id
                              where s.CreatedAt >= startDt && s.CreatedAt <= endDt
                              select new SaleReportRowDto
                              {
                                  SaleId = s.Id,
                                  SaleDate = s.CreatedAt,
                                  ClientName = c.Name,
                                  ProductName = p.Name,
                                  Quantity = si.Quantity,
                                  UnitPrice = si.UnitPrice,
                                  LineTotal = si.Quantity * si.UnitPrice
                              }).ToListAsync(cancellationToken);

            return rows;
        }
    }
}