using Ardalis.GuardClauses;
using SalesControl.Domain.SaleItemAggregate;

namespace SalesControl.Domain.SaleAggregate
{
    public class Sale : EntityBase<Guid>, IAggregateRoot
    {
        public Sale(Guid clientId, IEnumerable<SaleItem> items)
        {
            Guard.Against.Default(clientId, nameof(clientId));
            Guard.Against.Null(items, nameof(items));

            var list = items.ToList();
            Guard.Against.NullOrEmpty(list, nameof(items));

            Id = Guid.NewGuid();
            ClientId = clientId;
            Items = list.AsReadOnly();
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public Guid ClientId { get; private set; }
        public IReadOnlyList<SaleItem> Items { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public decimal Total => Items?.Sum(i => i.Subtotal) ?? 0m;
    }
}
