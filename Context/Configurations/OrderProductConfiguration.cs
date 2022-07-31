using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work_with_orders.Entities;

namespace Work_with_orders.Context.Configurations;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(op => new { op.OrderId, op.ProductId });


        builder.HasOne(op => op.Order)
            .WithMany(o => o.OrderProduct)
            .HasForeignKey(op => op.OrderId);

        builder.HasOne(op => op.Product)
            .WithMany(p => p.OrderProduct)
            .HasForeignKey(op => op.ProductId);
        
        builder.HasCheckConstraint("CK_Quantity", "\"Quantity\" > 0");
    }
}