using Ardalis.GuardClauses;
using SalesControl.Domain.SaleItemAggregate;

namespace SalesControl.Domain.SaleAggregate
{
    public class Sale : EntityBase<Guid>, IAggregateRoot
    {
        private readonly List<SaleItem> _items = new();
        public IReadOnlyList<SaleItem> Items => _items.AsReadOnly();

        public Sale(Guid clientId)
        {
            Guard.Against.Default(clientId, nameof(clientId));

            Id = Guid.NewGuid();
            ClientId = clientId;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public Guid ClientId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public decimal Total => _items.Sum(i => i.Subtotal);

        public void AddItem(Guid productId, int quantity, decimal unitPrice)
        {
            Guard.Against.Default(productId, nameof(productId));
            Guard.Against.NegativeOrZero(quantity, nameof(quantity));
            Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice));

            // evita duplicidade por productId
            var existing = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
            }
            else
            {
                var item = new SaleItem(productId, quantity, unitPrice);
                _items.Add(item);
            }

            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateItemQuantity(Guid saleItemId, int newQuantity)
        {
            var item = _items.FirstOrDefault(i => i.Id == saleItemId)
                ?? throw new InvalidOperationException("Item não encontrado na venda.");

            item.UpdateQuantity(newQuantity);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void RemoveItem(Guid saleItemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == saleItemId)
                ?? throw new InvalidOperationException("Item não encontrado na venda.");

            _items.Remove(item);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
