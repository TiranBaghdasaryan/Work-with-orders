namespace Work_with_orders.Commands.Interfaces;

public interface IDeleteProductExecutor : ICommand
{
    public IDeleteProductExecutor WithParameter(long parameter);
}