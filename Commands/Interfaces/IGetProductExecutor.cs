namespace Work_with_orders.Commands.Interfaces;

public interface IGetProductExecutor : ICommand
{
    public IGetProductExecutor WithParameter(long parameter);
}