namespace Work_with_orders.Commands.Executors.ProductExecutors.GetProduct;

public interface IGetProductExecutor : ICommand
{
    public IGetProductExecutor WithParameter(long parameter);
}