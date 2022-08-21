using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Options;

namespace Work_with_orders.DependencyInjection;

public static class DatabaseConfiguration
{
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(
            options => { options.UseNpgsql(ConnectionStrings.ConnectionPostgreSQL); },
            ServiceLifetime.Transient);
    }
}