namespace Work_with_orders.Commands.Interfaces;

public interface IBlockUserExecutor : ICommand
{
    public IBlockUserExecutor WithParameter(long id);
}