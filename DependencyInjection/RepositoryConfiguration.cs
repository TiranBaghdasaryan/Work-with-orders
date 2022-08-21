using Work_with_orders.Repositories;
using Work_with_orders.Repositories.BasketProductRepo;
using Work_with_orders.Repositories.OrderProductRepo;

namespace Work_with_orders.DependencyInjection;

public static class RepositoryConfiguration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<OrderRepository>();
        services.AddScoped<BasketRepository>();
        services.AddScoped<BasketProductRepository>();
        services.AddScoped<OrderProductRepository>();
    }
}