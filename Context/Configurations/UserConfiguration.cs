using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work_with_orders.Entities;
using Work_with_orders.Enums;

namespace Work_with_orders.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.Id).UseIdentityAlwaysColumn();
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);
        builder.Property(u => u.Address).IsRequired().HasMaxLength(50);
        builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Role).HasDefaultValue(Role.User);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
        builder.Property(u => u.IsVerified).HasDefaultValue(0);
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.Balance).HasDefaultValue(0);

        builder.HasKey(u => u.Id);
        builder.HasCheckConstraint("CK_Balance", "\"Balance\" >= 0");

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasMany(u => u.Orders)
            .WithOne(u => u.User)
            .HasForeignKey(o => o.UserId);
    }
}