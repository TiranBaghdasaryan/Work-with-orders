namespace Work_with_orders.Commands.Executors.AdminExecutor.BlockUser;

public interface IBlockUserExecutor : ICommand
{
    public IBlockUserExecutor WithParameter(long id);
}