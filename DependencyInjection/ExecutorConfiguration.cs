using Work_with_orders.Commands.Executors.AdminExecutor;
using Work_with_orders.Commands.Executors.AdminExecutor.BlockUser;
using Work_with_orders.Commands.Executors.AdminExecutor.UnblockUser;
using Work_with_orders.Commands.Executors.ProductExecutors.CreateProduct;
using Work_with_orders.Commands.Executors.ProductExecutors.GetProduct;
using Work_with_orders.Commands.Executors.ProductExecutors.UpdateProduct;

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