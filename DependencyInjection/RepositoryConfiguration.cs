using Work_with_orders.Repositories;
using Work_with_orders.Repositories.Implementations;
using Work_with_orders.Repositories.Interfaces;

namespace Work_with_orders.DependencyInjection;

public static class RepositoryConfiguration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IOrderRepository,OrderRepository>();
        services.AddScoped<IBasketRepository,BasketRepository>();
        services.AddScoped<IBasketProductRepository,BasketProductRepository>();
        services.AddScoped<IOrderProductRepository,OrderProductRepository>();
    }
}