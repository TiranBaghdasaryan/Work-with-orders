using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work_with_orders.Entities;

namespace Work_with_orders.Context.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.Id).UseIdentityAlwaysColumn();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Price).IsRequired().HasDefaultValue(0);
        builder.Property(p => p.Description).IsRequired().HasDefaultValue("None Description");


        builder.HasCheckConstraint("CK_Price", "\"Price\" >= 0");
        builder.HasKey(u => u.Id);
    }
}