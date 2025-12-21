using Ardalis.GuardClauses;

namespace SalesControl.Domain.ProductAggregate
{
    public class Product : EntityBase<Guid>, IAggregateRoot
    {
        public Product(string name, decimal price, int stock, string? description = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NegativeOrZero(price, nameof(price));
            Guard.Against.Negative(stock, nameof(stock));

            Id = Guid.NewGuid();
            Name = name.Trim();
            Price = price;
            Stock = stock;
            Description = description?.Trim();
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public void UpdatePrice(decimal newPrice)
        {
            Guard.Against.NegativeOrZero(newPrice, nameof(newPrice));
            Price = newPrice;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateName(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Name = name.Trim();
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateDescription(string? description)
        {
            Description = description?.Trim();
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void IncreaseStock(int quantity)
        {
            Guard.Against.NegativeOrZero(quantity, nameof(quantity));
            Stock += quantity;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void DecreaseStock(int quantity)
        {
            Guard.Against.NegativeOrZero(quantity, nameof(quantity));
            if (quantity > Stock) throw new InvalidOperationException("Insufficient stock.");
            Stock -= quantity;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
