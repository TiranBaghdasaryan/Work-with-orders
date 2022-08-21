using Work_with_orders.Commands.Executors.AdminExecutor;
using Work_with_orders.Commands.Implementations;
using Work_with_orders.Commands.Interfaces;

namespace Work_with_orders.DependencyInjection;

public static class ExecutorConfiguration
{

    public static void AddExecutors(this IServiceCollection services)
    {
        services.AddScoped<IGetProductExecutor, GetProductExecutor>();
        services.AddScoped<ICreateProductExecutor, CreateProductExecutor>();
        services.AddScoped<IUpdateProductExecutor, UpdateProductExecutor>();

        services.AddScoped<IFillUpUserBalanceExecutor, FillUpUserBalanceExecutor>();
        services.AddScoped<IBlockUserExecutor, BlockUserExecutor>();
        services.AddScoped<IUnblockUserExecutor, UnblockUserExecutor>();
    }
    

}