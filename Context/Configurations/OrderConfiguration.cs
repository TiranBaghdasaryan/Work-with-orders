using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work_with_orders.Entities;
using Work_with_orders.Enums;

namespace Work_with_orders.Context.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.Property(o => o.Id).UseIdentityAlwaysColumn();
        builder.Property(o => o.Status).HasDefaultValue(OrderStatus.InProcess);

        builder.HasKey(o => o.Id);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
    }
}