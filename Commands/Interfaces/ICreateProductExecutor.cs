using Work_with_orders.Models.RequestModels;

namespace Work_with_orders.Commands.Interfaces;

public interface ICreateProductExecutor : ICommand
{
    public ICreateProductExecutor WithParameter(CreateProductRequestModel parameter);
}