namespace Work_with_orders.Commands.Executors;

public interface IGetProductExecutor : ICommand
{
    public IGetProductExecutor WithParameter(long parameter);
}