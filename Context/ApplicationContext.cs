using Microsoft.EntityFrameworkCore;
using Work_with_orders.Entities;

namespace Work_with_orders.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order>? Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(u => { u.HasKey(e => e.Id); });
    }
}