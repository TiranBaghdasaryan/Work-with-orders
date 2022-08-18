using Work_with_orders.Models.ProductModels.CreateProduct;

namespace Work_with_orders.Commands.Executors;

public interface ICreateProductExecutor : ICommand
{
    public ICreateProductExecutor WithParameter(CreateProductRequestModel parameter);
}