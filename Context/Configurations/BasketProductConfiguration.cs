using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work_with_orders.Entities;

namespace Work_with_orders.Context.Configurations;

public class BasketProductConfiguration : IEntityTypeConfiguration<BasketProduct>
{
    public void Configure(EntityTypeBuilder<BasketProduct> builder)
    {
        
        builder.ToTable("BasketProduct");
        
        builder.HasKey(bp => new { bp.BasketId, bp.ProductId });

        builder.HasOne(bp => bp.Basket)
            .WithMany(b => b.BasketProduct)
            .HasForeignKey(bp => bp.BasketId);

        builder.HasOne(bp => bp.Product)
            .WithMany(b => b.BasketProduct)
            .HasForeignKey(bp => bp.ProductId);
        
    }
}