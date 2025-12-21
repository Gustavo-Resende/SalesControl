using Ardalis.GuardClauses;

namespace SalesControl.Domain.ClientAggregate
{
    public class Client : EntityBase<Guid>, IAggregateRoot
    {
        public Client(string name, string email, string? phone = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrWhiteSpace(email, nameof(email));

            Id = Guid.NewGuid();
            Name = name.Trim();
            Email = email.Trim();
            Phone = phone?.Trim();
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string? Phone { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public void UpdateContact(string name, string email, string? phone = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrWhiteSpace(email, nameof(email));

            Name = name.Trim();
            Email = email.Trim();
            Phone = phone?.Trim();
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
