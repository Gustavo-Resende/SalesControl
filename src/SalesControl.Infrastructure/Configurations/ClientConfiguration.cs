using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesControl.Domain.ClientAggregate;

namespace SalesControl.Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            builder.Property(c => c.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            builder.Property(c => c.Phone).HasColumnName("phone").HasMaxLength(50);
            builder.Property(c => c.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(c => c.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("now()").IsRequired();

            // O índice de unicidade por LOWER(email) foi criado no script SQL externo.
            // Índice simples para consultas via EF.
            builder.HasIndex(c => c.Email).HasDatabaseName("ix_clients_email");
        }
    }
}