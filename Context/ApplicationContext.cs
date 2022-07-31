using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context.Configurations;
using Work_with_orders.Entities;

namespace Work_with_orders.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order>? Orders { get; set; }
    public DbSet<User>? Users { get; set; }


   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}