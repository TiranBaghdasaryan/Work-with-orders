using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work_with_orders.Entities;

namespace Work_with_orders.Context.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{

    public void Configure(EntityTypeBuilder<Basket> builder)
    {

        builder.ToTable("Baskets");
       
        builder.Property(b => b.Id).UseIdentityAlwaysColumn();

        builder.HasKey(b => b.Id);

        builder.HasOne(o => o.User)
            .WithOne(u => u.Basket)
            .HasForeignKey<Basket>(b => b.UserId);

    }
}