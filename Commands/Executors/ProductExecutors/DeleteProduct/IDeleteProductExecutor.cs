namespace Work_with_orders.Commands.Executors.ProductExecutors.DeleteProduct;

public interface IDeleteProductExecutor : ICommand
{
    public IDeleteProductExecutor WithParameter(long parameter);
}