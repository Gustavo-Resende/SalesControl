using System;
using Ardalis.GuardClauses;

namespace SalesControl.Domain.SaleItemAggregate
{
    public class SaleItem : EntityBase<Guid>
    {
        public SaleItem(Guid productId, int quantity, decimal unitPrice)
        {
            Guard.Against.Default(productId, nameof(productId));
            Guard.Against.NegativeOrZero(quantity, nameof(quantity));
            Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice));

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; } // snapshot do preço no momento da venda
        public decimal Subtotal => UnitPrice * Quantity;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public void UpdateQuantity(int newQuantity)
        {
            Guard.Against.NegativeOrZero(newQuantity, nameof(newQuantity));
            Quantity = newQuantity;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateUnitPrice(decimal newPrice)
        {
            Guard.Against.NegativeOrZero(newPrice, nameof(newPrice));
            UnitPrice = newPrice;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
