using Work_with_orders.Models.RequestModels;

namespace Work_with_orders.Commands.Interfaces;

public interface IUpdateProductExecutor : ICommand
{
    public IUpdateProductExecutor WithParameter(UpdateProductRequestModel parameter);
}