using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesControl.Domain.SaleItemAggregate;

namespace SalesControl.Infrastructure.Configurations
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("sale_items");
            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnName("id");

            // shadow FK para sale_id
            builder.Property<System.Guid>("sale_id").HasColumnName("sale_id").IsRequired();

            builder.Property(si => si.ProductId).HasColumnName("product_id").IsRequired();
            builder.Property(si => si.Quantity).HasColumnName("quantity").IsRequired();
            builder.Property(si => si.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 4).IsRequired();
            builder.Property(si => si.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(si => si.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("now()").IsRequired();

            // Subtotal é calculado no domínio; o banco declara 'line_total' como coluna gerada
            builder.Ignore(si => si.Subtotal);

            builder.Property<decimal>("line_total")
                   .HasColumnName("line_total")
                   .HasPrecision(18, 4)
                   .HasComputedColumnSql("quantity * unit_price", stored: true);

            builder.HasOne<SalesControl.Domain.ProductAggregate.Product>()
                   .WithMany()
                   .HasForeignKey(si => si.ProductId)
                   .HasConstraintName("fk_saleitems_product")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}