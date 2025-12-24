using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesControl.Domain.SaleAggregate;

namespace SalesControl.Infrastructure.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sales");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");
            builder.Property(s => s.ClientId).HasColumnName("client_id").IsRequired();
            builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("now()").IsRequired();

            // Total é calculado no domínio/consulta
            builder.Ignore(s => s.Total);

            builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey("sale_id")
                   .HasPrincipalKey(s => s.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}